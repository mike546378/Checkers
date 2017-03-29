using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Checkers
{
    class Multiplayer
    {

        private String ip;
        private int port;
        private Socket client;
        private Thread receiveWorker;
        private Player_Remote player;
        private String gameID;

        //Initilization of server connection
        public Multiplayer(Player_Remote player)
        {
            this.player = player;
            ip = player.getManager().getForm().getIP();
            port = Convert.ToInt32(player.getManager().getForm().getPort());
            gameID = player.getManager().getForm().getID();

            player.getManager().updateStatus("Connecting to server");
            Console.WriteLine("Connecting to server...");
            connect(ip, port); //Attempt server connection
        }


        //Attempts a connection to the server
        public Boolean connect(String ip, int port)
        {
            //Setting target ip and port
            IPAddress ipAddr = IPAddress.Parse(ip);
            IPEndPoint remoteEndPoint = new IPEndPoint(ipAddr, port);

            client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(remoteEndPoint);
                receive(); //Connection successfull, start receiving messages
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to server"); //Connection failed
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


        //Sends server an initilization string
        public void initString()
        {
            String team = "black";
            if (player.getManager().getForm().getTeam() != checkerPiece.Team.DARK)
                team = "white";
            send("init`" + gameID + "`" + team);
        }

        //Async messageBox for testing
        public void asyncMsgBox(String msg)
        {
            new Thread(() => { System.Windows.Forms.MessageBox.Show(msg); }).Start();
        }

        //Sends server a message
        public void send(String msg)
        {
            Console.WriteLine("Sending: CKSHND`" + msg);
            new Thread(() => { byte[] toSend = Encoding.ASCII.GetBytes("CKSHND`" + msg + "\n");  client.Send(toSend); }).Start();
            
        }

        //Receives messages in seperate thread
        public void receive()
        {
            receiveWorker = new Thread(() =>
            {
                byte[] bytes = new byte[1024];
                try
                {
                    while (true) //Continuisly receive messages from all clients
                    {
                        if (player.getManager().getForm().IsDisposed) //Kill the connection if main program is closed
                        {
                            this.closeConnection();
                            return;
                        }
                        if (client.Connected)
                        {
                            int bytesRec = client.Receive(bytes);
                            String msg = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            if (msg.Contains("CKSHND`")) //Callsign to ensure message is ment for this program
                            {
                                foreach (String m in msg.Split(new String[] { "|ET|" }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    String input = m;
                                    input = input.Substring(7); //Remove callsign 
                                    switch (input.ToLower())
                                    {
                                        case "connected":
                                            initString(); //Sends the server an initilization string
                                            Console.WriteLine("Connected, Waiting for opponent");
                                            break;
                                        case "status=waiting":  //States that you are wating on the other client
                                            player.getManager().getForm().displaySplashScreen(Checkers.SplashType.WAITING);
                                            break;
                                        case "status=start":    //Starts the game
                                            player.getManager().startGame();
                                            player.getManager().getForm().hideSplashScreen();
                                            break;
                                        case "nextturn":    //Ends remote players turn, moves onto local players turn
                                            player.getManager().nextTurn();
                                            break;
                                        case "disconnect":
                                            player.getManager().getForm().displaySplashScreen(Checkers.SplashType.DISCONNECTED);
                                            break;
                                    }
                                    if (input.ToLower().Contains("move="))  //Moves a specified piece from one location to another
                                    {
                                        input = input.Substring(input.IndexOf("=") + 1);
                                        String[] coords = input.Split('|');
                                        int[] oldCoords = Array.ConvertAll(coords[0].Split(','), s => int.Parse(s)); //Retrieve old coords as int array
                                        int[] newCoords = Array.ConvertAll(coords[1].Split(','), s => int.Parse(s)); //Retrieve old coords as int array
                                        player.move(oldCoords, newCoords);
                                    }
                                    else if (input.ToLower().Contains("del="))  //deleted a specified piece from gameplay
                                    {
                                        input = input.Substring(input.IndexOf("=") + 1);
                                        int[] coords = Array.ConvertAll(input.Split(','), s => int.Parse(s)); //parse input coords into int array
                                        player.getManager().getCheckerPiece(player.getManager().getTile(coords[0], coords[1])).remove();
                                    }
                                    else if (input.ToLower().Contains("king="))  //Upgrades a specified piece to king
                                    {
                                        input = input.Substring(input.IndexOf("=") + 1);
                                        int[] coords = Array.ConvertAll(input.Split(','), s => int.Parse(s)); //parse input coords into int array
                                        player.getManager().addKing(player.getTeam(), player.getManager().getTile(coords[0], coords[1]));
                                    }
                                    else if (input.ToLower().Contains("end="))  //Message to end the game
                                    {
                                        input = input.Substring(input.IndexOf("=") + 1);
                                        bool won = Convert.ToBoolean(input);
                                        player.getManager().endGame(won);
                                        this.closeConnection();
                                    }
                                }
                                Console.WriteLine(msg);
                            }
                        }
                        else
                        {
                            Thread.Sleep(1000); //Sleep for 1 second if client is not connected to server
                        }
                    }
                }
                catch (Exception ex)
                {
                    //asyncMsgBox("Error");
                    Console.WriteLine("Error occoured, Closing receiver thread"); //Usually fired when thread is being aborted
                    Console.Write(ex.StackTrace);
                    this.closeConnection();
                }
            });
            receiveWorker.Start();
        }

        //Cleans up the connection before closing window
        public void closeConnection()
        {
            if (receiveWorker != null)
                if (receiveWorker.IsAlive)
                    receiveWorker.Abort();
            client.Close();
        }


    }
}

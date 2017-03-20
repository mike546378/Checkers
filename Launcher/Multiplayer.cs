using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graded_Unit_Launcher
{
    class Multiplayer
    {
        private String ip = "localhost";
        private int port = 5463;
        private System.Windows.Forms.Label status;
        private Socket client;
        private Thread receiveWorker;

        //Initilization of server connection
        public Multiplayer(System.Windows.Forms.Label status)
        {
            this.status = status;
            updateStatus("Connecting to server...");
            string[] settings = File.ReadAllLines("settings.txt"); //Reading from settings file
            ip = settings[0];
            port = Convert.ToInt32(settings[1]);

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
                updateStatus("Error connecting to server"); //Connection failed
                Console.WriteLine(ex.ToString());
                return false;
            }
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
                        if (client.Connected)
                        {
                            int bytesRec = client.Receive(bytes);
                            String msg = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            if (msg.Contains("CKSHND`")) //Callsign to ensure message is ment for this program
                            {
                                msg = msg.Substring(7); //Remove callsign
                                if (msg.ToLower().Equals("connected")) //Parse messages
                                {
                                    updateStatus("Connected, Waiting for opponent");
                                }
                            }
                            Console.WriteLine(msg);
                        }
                        else
                        {
                            Thread.Sleep(1000); //Sleep for 1 second if client is not connected to server
                        }    
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Closing receiver thread"); //Usually fired when thread is being aborted
                    updateStatus("An error occoured");
                }
            });
            receiveWorker.Start();
        }

        //Cleans up the connection before closing window
        public void closeConnection()
        {
            if(receiveWorker != null)
                if(receiveWorker.IsAlive)
                    receiveWorker.Abort();
            client.Close();
        }

        //Thread-safe method to update status label
        private void updateStatus(String msg)
        {
            this.status.BeginInvoke((MethodInvoker)delegate () { this.status.Text = msg; ; });
        }
    }
}

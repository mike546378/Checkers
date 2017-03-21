using Checkers.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    /*
     * WARNING: The following program contains code consisting of many hacks,
     * confusing refrences and overall convoluted methods. A saftey pig is
     * provided below for your own benifit!
     *                         _
     * _._ _..._ .-',     _.._(`))
     *'-. `     '  /-._.-'    ',/
     *   )         \            '.
     *  / _    _    |             \
     * |  a    a    /              |
     * \   .-.                     ;  
     *  '-('' ).-'       ,'       ;
     *     '-;           |      .'
     *        \           \    /
     *        | 7  .__  _.-\   \
     *        | |  |  ``/  /`  /
     *       /,_|  |   /,_/   /
     *          /,_/      '`-'
     */

    public partial class Checkers : Form
    {

        //Instance of BoardManager handles all interactions with the board
        BoardManager board;
        GameType gameType = GameType.Unset;
        String gameID = null;
        String team = null;

        //Different game types
        public enum GameType
        {
            Unset = 0,
            SinglePlayer = 1,
            LocalMultiplayer = 2,
            OnlineMultiplayer = 3,
        }

        //Initilization through constructor
        public Checkers(string[] args)
        {
            InitializeComponent();            
            board = new BoardManager(this);
            picTable.SendToBack();
            updatePicTableLocation();
            parseArgs(args);
            if (gameType == GameType.Unset) //Exit application and return to launcher if no GameType was specified
           {
                System.Diagnostics.Process.Start("Launcher.exe");
                this.Close();
                return;
            }
            board.createPlayers(null, checkerPiece.Team.DARK);
            board.nextTurn();
        }


        //Return gameType
        public GameType getGameType()
        {
            return gameType;
        }

        //Decyphers command line arguments and takes action on them
        public void parseArgs(string[] args)
        {
            foreach (String arg in args)
            {
                switch (getArgComponent(arg).ToLower())
                {
                   
                    case "--gametype":    //Setting the game type
                        switch (getArgValue(arg).ToUpper())
                        {
                            case "LMP":
                                gameType = GameType.LocalMultiplayer;
                                break;
                            case "SP":
                                gameType = GameType.SinglePlayer;
                                break;
                            case "OMP":
                                gameType = GameType.OnlineMultiplayer;
                                break;
                        }
                        break;

                    case "--left":    //Setting screen location
                        this.Location = new Point(Convert.ToInt32(getArgValue(arg)), this.Location.Y);
                        break;
                    case "--top":    //Setting screen location
                        this.Location = new Point(this.Location.X, Convert.ToInt32(getArgValue(arg)));
                        break;
                }
            }
        }
        private String getArgValue(String arg) { //Gets value of an argument
            String a = arg.Substring(arg.IndexOf("=") + 1);
            return a;
        }
        private String getArgComponent(String arg)
        { //Gets value of an argument
            String a = arg.Substring(0,arg.IndexOf("="));
            return a;
        }

        //Redraw board when window resized
        private void Checkers_Resize(object sender, EventArgs e)
        {
            board.redrawBoard();
            updatePicTableLocation();
        }

        //Updates table image according to board size
        private void updatePicTableLocation()
        {
            picTable.Location = new Point(board.getBoard().Location.X - 30, 0);
            picTable.Size = new Size(board.getBoard().Size.Width + 60, this.ClientSize.Height);
        }
    }
}

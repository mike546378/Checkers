using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graded_Unit_Launcher
{
    public partial class frmLauncher : Form
    {
        Multiplayer multiplayer = null; //Set if user wishes to play online multiplayer

        //initilization of form
        public frmLauncher()
        {
            InitializeComponent();
            pnlMultiplayer.Visible = false;
        }

        //Button to back out of multiplayer connectivity
        private void lblBack_Click(object sender, EventArgs e)
        {
            pnlMultiplayer.Visible = false;
            lblStatus.Text = "null";
            multiplayer.closeConnection();
            multiplayer = null;
        }

        //Launches single player game
        private void lblSinglePlayer_Click(object sender, EventArgs e)
        {
            launch(new String[] {"GameType=SP" });
        }

        //Launches game with given parameters
        private void launch(String[] args)
        {
            String arguments = "";
            foreach (String s in args) //Building program parameters into string
                arguments += " --" + s;

            //Position args
            arguments += "--Left=" + this.Location.X;
            arguments += "--Left=" + this.Location.Y;

            //Creating new process, adding filename and args, starting process
            Process Checkers = new Process();
            Checkers.StartInfo.FileName = "Checkers.exe";
            Checkers.StartInfo.Arguments = arguments;
            Checkers.Start();
            Application.Exit();
        }

        //Launches 2 player game
        private void lbl2Player_Click(object sender, EventArgs e)
        {
            launch(new String[] { "GameType=LMP" });
        }

        //Connects to server for online multiplayer game
        private void lblMultiplayer_Click(object sender, EventArgs e)
        {
            pnlMultiplayer.Visible = true;
            Thread mp = new Thread(() =>
            {
                multiplayer = new Multiplayer(this.lblStatus);
            });
            mp.Start();
        }

        private void lblQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

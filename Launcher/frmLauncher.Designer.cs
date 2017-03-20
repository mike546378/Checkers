namespace Graded_Unit_Launcher
{
    partial class frmLauncher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLauncher));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSinglePlayer = new System.Windows.Forms.Label();
            this.lbl2Player = new System.Windows.Forms.Label();
            this.lblMultiplayer = new System.Windows.Forms.Label();
            this.pnlHome = new System.Windows.Forms.Panel();
            this.pnlMultiplayer = new System.Windows.Forms.Panel();
            this.lblBack = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStausTxt = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblQuit = new System.Windows.Forms.Label();
            this.pnlHome.SuspendLayout();
            this.pnlMultiplayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Modern No. 20", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Yellow;
            this.lblTitle.Location = new System.Drawing.Point(134, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(398, 36);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CHECKERS LAUNCHER";
            // 
            // lblSinglePlayer
            // 
            this.lblSinglePlayer.AutoSize = true;
            this.lblSinglePlayer.BackColor = System.Drawing.Color.Transparent;
            this.lblSinglePlayer.Font = new System.Drawing.Font("Modern No. 20", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSinglePlayer.ForeColor = System.Drawing.Color.Yellow;
            this.lblSinglePlayer.Location = new System.Drawing.Point(19, 102);
            this.lblSinglePlayer.Name = "lblSinglePlayer";
            this.lblSinglePlayer.Size = new System.Drawing.Size(155, 36);
            this.lblSinglePlayer.TabIndex = 1;
            this.lblSinglePlayer.Text = "- 1 Player";
            this.lblSinglePlayer.Click += new System.EventHandler(this.lblSinglePlayer_Click);
            // 
            // lbl2Player
            // 
            this.lbl2Player.AutoSize = true;
            this.lbl2Player.BackColor = System.Drawing.Color.Transparent;
            this.lbl2Player.Font = new System.Drawing.Font("Modern No. 20", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2Player.ForeColor = System.Drawing.Color.Yellow;
            this.lbl2Player.Location = new System.Drawing.Point(19, 173);
            this.lbl2Player.Name = "lbl2Player";
            this.lbl2Player.Size = new System.Drawing.Size(155, 36);
            this.lbl2Player.TabIndex = 2;
            this.lbl2Player.Text = "- 2 Player";
            this.lbl2Player.Click += new System.EventHandler(this.lbl2Player_Click);
            // 
            // lblMultiplayer
            // 
            this.lblMultiplayer.AutoSize = true;
            this.lblMultiplayer.BackColor = System.Drawing.Color.Transparent;
            this.lblMultiplayer.Font = new System.Drawing.Font("Modern No. 20", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMultiplayer.ForeColor = System.Drawing.Color.Yellow;
            this.lblMultiplayer.Location = new System.Drawing.Point(19, 242);
            this.lblMultiplayer.Name = "lblMultiplayer";
            this.lblMultiplayer.Size = new System.Drawing.Size(310, 36);
            this.lblMultiplayer.TabIndex = 3;
            this.lblMultiplayer.Text = "- Online Multiplayer";
            this.lblMultiplayer.Click += new System.EventHandler(this.lblMultiplayer_Click);
            // 
            // pnlHome
            // 
            this.pnlHome.BackColor = System.Drawing.Color.Transparent;
            this.pnlHome.Controls.Add(this.pnlMultiplayer);
            this.pnlHome.Controls.Add(this.lblMultiplayer);
            this.pnlHome.Controls.Add(this.lbl2Player);
            this.pnlHome.Controls.Add(this.lblTitle);
            this.pnlHome.Controls.Add(this.lblSinglePlayer);
            this.pnlHome.Controls.Add(this.lblQuit);
            this.pnlHome.Location = new System.Drawing.Point(2, 0);
            this.pnlHome.Name = "pnlHome";
            this.pnlHome.Size = new System.Drawing.Size(670, 502);
            this.pnlHome.TabIndex = 4;
            // 
            // pnlMultiplayer
            // 
            this.pnlMultiplayer.BackColor = System.Drawing.Color.Transparent;
            this.pnlMultiplayer.Controls.Add(this.lblBack);
            this.pnlMultiplayer.Controls.Add(this.lblStatus);
            this.pnlMultiplayer.Controls.Add(this.lblStausTxt);
            this.pnlMultiplayer.Controls.Add(this.lblTitle2);
            this.pnlMultiplayer.Location = new System.Drawing.Point(0, 0);
            this.pnlMultiplayer.Name = "pnlMultiplayer";
            this.pnlMultiplayer.Size = new System.Drawing.Size(670, 502);
            this.pnlMultiplayer.TabIndex = 4;
            // 
            // lblBack
            // 
            this.lblBack.AutoSize = true;
            this.lblBack.BackColor = System.Drawing.Color.Transparent;
            this.lblBack.Font = new System.Drawing.Font("Modern No. 20", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBack.ForeColor = System.Drawing.Color.Yellow;
            this.lblBack.Location = new System.Drawing.Point(19, 242);
            this.lblBack.Name = "lblBack";
            this.lblBack.Size = new System.Drawing.Size(293, 36);
            this.lblBack.TabIndex = 7;
            this.lblBack.Text = "Back to main menu";
            this.lblBack.Click += new System.EventHandler(this.lblBack_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Modern No. 20", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Yellow;
            this.lblStatus.Location = new System.Drawing.Point(134, 109);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(74, 36);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "null";
            // 
            // lblStausTxt
            // 
            this.lblStausTxt.AutoSize = true;
            this.lblStausTxt.BackColor = System.Drawing.Color.Transparent;
            this.lblStausTxt.Font = new System.Drawing.Font("Modern No. 20", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStausTxt.ForeColor = System.Drawing.Color.Yellow;
            this.lblStausTxt.Location = new System.Drawing.Point(19, 109);
            this.lblStausTxt.Name = "lblStausTxt";
            this.lblStausTxt.Size = new System.Drawing.Size(118, 36);
            this.lblStausTxt.TabIndex = 5;
            this.lblStausTxt.Text = "Status: ";
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle2.Font = new System.Drawing.Font("Modern No. 20", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.ForeColor = System.Drawing.Color.Yellow;
            this.lblTitle2.Location = new System.Drawing.Point(134, 22);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(398, 36);
            this.lblTitle2.TabIndex = 4;
            this.lblTitle2.Text = "CHECKERS LAUNCHER";
            // 
            // lblQuit
            // 
            this.lblQuit.AutoSize = true;
            this.lblQuit.BackColor = System.Drawing.Color.Transparent;
            this.lblQuit.Font = new System.Drawing.Font("Modern No. 20", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuit.ForeColor = System.Drawing.Color.Yellow;
            this.lblQuit.Location = new System.Drawing.Point(19, 312);
            this.lblQuit.Name = "lblQuit";
            this.lblQuit.Size = new System.Drawing.Size(99, 36);
            this.lblQuit.TabIndex = 5;
            this.lblQuit.Text = "- Quit";
            this.lblQuit.Click += new System.EventHandler(this.lblQuit_Click);
            // 
            // frmLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(672, 502);
            this.Controls.Add(this.pnlHome);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmLauncher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checkers Launcher";
            this.pnlHome.ResumeLayout(false);
            this.pnlHome.PerformLayout();
            this.pnlMultiplayer.ResumeLayout(false);
            this.pnlMultiplayer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSinglePlayer;
        private System.Windows.Forms.Label lbl2Player;
        private System.Windows.Forms.Label lblMultiplayer;
        private System.Windows.Forms.Panel pnlHome;
        private System.Windows.Forms.Panel pnlMultiplayer;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblBack;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStausTxt;
        private System.Windows.Forms.Label lblQuit;
    }
}


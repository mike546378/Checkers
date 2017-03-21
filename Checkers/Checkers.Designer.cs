namespace Checkers
{
    partial class Checkers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Checkers));
            this.picTable = new System.Windows.Forms.PictureBox();
            this.picSplashScreen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSplashScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // picTable
            // 
            this.picTable.BackgroundImage = global::Checkers.Properties.Resources.TableTop1;
            this.picTable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picTable.Location = new System.Drawing.Point(305, 42);
            this.picTable.Name = "picTable";
            this.picTable.Size = new System.Drawing.Size(113, 175);
            this.picTable.TabIndex = 0;
            this.picTable.TabStop = false;
            // 
            // picSplashScreen
            // 
            this.picSplashScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picSplashScreen.Location = new System.Drawing.Point(32, 325);
            this.picSplashScreen.Name = "picSplashScreen";
            this.picSplashScreen.Size = new System.Drawing.Size(303, 164);
            this.picSplashScreen.TabIndex = 1;
            this.picSplashScreen.TabStop = false;
            this.picSplashScreen.Visible = false;
            this.picSplashScreen.Click += new System.EventHandler(this.picSplashScreen_Click);
            // 
            // Checkers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Checkers.Properties.Resources.grass;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(524, 501);
            this.Controls.Add(this.picSplashScreen);
            this.Controls.Add(this.picTable);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(540, 540);
            this.Name = "Checkers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checkers";
            this.Resize += new System.EventHandler(this.Checkers_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSplashScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picTable;
        private System.Windows.Forms.PictureBox picSplashScreen;
    }
}


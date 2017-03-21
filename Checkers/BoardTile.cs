using Checkers.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    //Expands on PictureBox to give more methods for Board Tiles
    class BoardTile : PictureBox
    {
        private int x;
        private int y;
        private bool highlighted = false;
        private BoardManager manager;

        public BoardTile(BoardManager man, int xCoord, int yCoord)
        {
            manager = man;
            x = xCoord;
            y = yCoord;
            this.Click += new EventHandler(tile_Click);
        }

        //Getters for coords
        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }


        public void tile_Click(object sender, EventArgs e)
        {
            if(this.highlighted)
                manager.moveRequest(this);
        }

            //Changes highlight state of tile
            public void highlight(bool state)
        {
            if (state)
            {
                this.BackgroundImage = Resources.Tile_White_Highlighted;
                highlighted = true;
            }
            if (!state && highlighted)
            {
                this.BackgroundImage = Resources.Tile_White;
                highlighted = false;
            }
        }
    }
}

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


        public BoardTile(int xCoord, int yCoord)
        {
            x = xCoord;
            y = yCoord;
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
    }
}

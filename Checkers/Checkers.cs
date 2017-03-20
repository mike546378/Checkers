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

        //Initilization through constructor
        public Checkers(string[] args)
        {
            InitializeComponent();
            board = new BoardManager(this);
            picTable.SendToBack();
            updatePicTableLocation();

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

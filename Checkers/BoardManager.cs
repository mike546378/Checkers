using Checkers.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    class BoardManager
    {
        //2D array of PictureBoxes to store the board pieces
        BoardTile[,] board = new BoardTile[10, 10];
        Panel pnlBoard;
        Checkers form;
        List<checkerPiece> pieces = new List<checkerPiece>();
        List<Player> players = new List<Player>();

        public BoardManager(Checkers sender)
        {
            form = sender;
            createBoard();
            populateBoard();
            players.Add(new Player(checkerPiece.Team.WHITE, this));
        }

        //Creates the boards layout
        private void createBoard()
        {
            pnlBoard = new DoubleBufferedPanel();
            form.Controls.Add(pnlBoard);
            pnlBoard.SuspendLayout();
            Boolean color = false;
            Image black = Resources.Tile_Dark;
            Image white = Resources.Tile_White;

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    board[x, y] = new BoardTile(x,y);
                    board[x, y].Size = new System.Drawing.Size(45, 45);
                    board[x, y].AutoSize = false;
                    if (color) { board[x, y].BackgroundImage = black; } else { board[x, y].BackgroundImage = white; }
                    color = !color;
                    board[x, y].BackgroundImageLayout = ImageLayout.Stretch;
                    pnlBoard.Controls.Add(board[x, y]);
                }
                color = !color;
            }
            redrawBoard();
            pnlBoard.ResumeLayout();
        }


        //Repositions and resizes all elements of the board
        public void redrawBoard()
        {
            int height = Convert.ToInt32(form.ClientSize.Height * 0.9);
            int width = Convert.ToInt32(form.ClientSize.Width * 0.9);

            if (height < width) { width = height; } else { height = width; }
            pnlBoard.Size = new Size(Convert.ToInt32((width / 10) * 10), Convert.ToInt32((height / 10) * 10)); //Small hack to ensure panel size is exactly correct for 10x10 board pieces
            pnlBoard.Location = new Point((form.ClientSize.Width - pnlBoard.ClientSize.Width) / 2, Convert.ToInt32(((form.ClientSize.Height - pnlBoard.Height)) / 2));
            for (int y = 0; y < 10; y++)
                for (int x = 0; x < 10; x++)
                {
                    board[x, y].Size = new Size(Convert.ToInt32(pnlBoard.Width / 10), Convert.ToInt32(pnlBoard.Height / 10));
                    board[x, y].Location = new Point(Convert.ToInt32(pnlBoard.Width / 10) * x, Convert.ToInt32(pnlBoard.Height / 10) * y);
                }

            foreach (checkerPiece piece in pieces)
            {
                piece.setSize(Convert.ToInt32(pnlBoard.Width / 10), Convert.ToInt32(pnlBoard.Width / 10));
                piece.updateLoc();
            }
        }


        //Method retreives the tile at given coords
        public BoardTile getTile(int x, int y)
        {
            try
            {
                BoardTile tile = board[x, y];
                return tile;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Method creates initial checkers pieces
        public void populateBoard()
        {
            pnlBoard.SuspendLayout();
            for (int y = 0; y < 2; y++)
                for (int x = 0; x < 5; x++)
                {
                    checkerPiece piece = new checkerPiece(this, checkerPiece.Team.WHITE);
                    piece.move(getTile(x*2, y));
                    if (y==1) //Offsetting by 1 pos on x-axis
                        piece.move(getTile(x*2+1, y));
                    pieces.Add(piece);
                }

            for (int y = 8; y < 10; y++)
                for (int x = 0; x < 5; x++)
                {
                    checkerPiece piece = new checkerPiece(this, checkerPiece.Team.DARK);
                    piece.move(getTile(x*2, y));
                    if(y==9) //Offsetting by 1 pos on x-axis
                        piece.move(getTile(x*2+1, y));
                    pieces.Add(piece);
                }

            this.redrawBoard();
            pnlBoard.ResumeLayout();
        }


        //Checks if a tile contains a checker piece
        public Boolean getTileContainsPiece(BoardTile tile)
        {
            foreach (checkerPiece piece in pieces)
            {
                if (piece.getTile() == tile)
                    return true;
            }
            return false;
        }


        //Returns the checker piece on a specific tile
        public checkerPiece getCheckerPiece(BoardTile tile)
        {
            foreach (checkerPiece piece in pieces)
            {
                if (piece.getTile() == tile)
                    return piece;
            }
            return null;
        }

        //Returns the main form
        public Checkers getForm()
        {
            return form;
        }

        //Returns the panel board
        public Panel getBoard()
        {
            return pnlBoard;
        }

        //Returns all the checkers pieces
        public List<checkerPiece> getAllPieces()
        {
            return pieces;
        }

        //Returns all the checkers pieces for given team
        public List<checkerPiece> getAllPieces(checkerPiece.Team team)
        {
            List<checkerPiece> validPieces = new List<checkerPiece>();
            foreach (checkerPiece piece in pieces)
                if (piece.getTeam() == team)
                    validPieces.Add(piece);
            return validPieces;
        }
    }
}

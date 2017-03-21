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
        BoardTile[,] board = new BoardTile[8, 8];
        checkerPiece.Team turn = checkerPiece.Team.WHITE;
        Panel pnlBoard;
        Checkers form;
        List<checkerPiece> pieces = new List<checkerPiece>();
        List<Player> players = new List<Player>();

        public BoardManager(Checkers sender)
        {
            form = sender;
            createBoard();
            populateBoard();
        }


        //Creats player objects based on game type
        public void createPlayers(String gameID, checkerPiece.Team team)
        {
            checkerPiece.Team enemyTeam = checkerPiece.Team.WHITE;
            if (team == enemyTeam)
                enemyTeam = checkerPiece.Team.DARK;

            switch (form.getGameType())
            {
                case Checkers.GameType.SinglePlayer:
                    players.Add(new Player(team, this));
                    players.Add(new Player_AI(enemyTeam, this));
                    break;
                case Checkers.GameType.LocalMultiplayer:
                    players.Add(new Player(team, this));
                    players.Add(new Player(enemyTeam, this));
                    break;
                case Checkers.GameType.OnlineMultiplayer:
                    players.Add(new Player(team, this));
                    players.Add(new Player(enemyTeam, this));
                    break;
            }
        }

        //Creates the boards layout
        private void createBoard()
        {
            pnlBoard = new DoubleBufferedPanel();
            form.Controls.Add(pnlBoard);
            pnlBoard.SuspendLayout();
            Boolean color = true;
            Image black = Resources.Tile_Dark;
            Image white = Resources.Tile_White;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    board[x, y] = new BoardTile(this, x,y);
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


        //Moves onto the next players turn
        public void nextTurn()
        {
            foreach (checkerPiece piece in pieces)
            {
                piece.clearHighlight();
            }
            if (turn == checkerPiece.Team.WHITE)
            {
                turn = checkerPiece.Team.DARK;
                updateStatus("Blacks Turn");
            }
            else {
                turn = checkerPiece.Team.WHITE;
                updateStatus("Whites Turn");
            }
            if (players[0].getTeam() == turn) { players[0].startTurn(); } else { players[1].startTurn(); }
        }
            
        //Repositions and resizes all elements of the board
        public void redrawBoard()
        {
            int height = Convert.ToInt32(form.ClientSize.Height * 0.9);
            int width = Convert.ToInt32(form.ClientSize.Width * 0.9);

            if (height < width) { width = height; } else { height = width; }
            pnlBoard.Size = new Size(Convert.ToInt32((width / 8) * 8), Convert.ToInt32((height / 8) * 8)); //Small hack to ensure panel size is exactly correct for 10x10 board pieces
            pnlBoard.Location = new Point((form.ClientSize.Width - pnlBoard.ClientSize.Width) / 2, Convert.ToInt32(((form.ClientSize.Height - pnlBoard.Height)) / 2));
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                {
                    board[x, y].Size = new Size(Convert.ToInt32(pnlBoard.Width / 8), Convert.ToInt32(pnlBoard.Height / 8));
                    board[x, y].Location = new Point(Convert.ToInt32(pnlBoard.Width / 8) * x, Convert.ToInt32(pnlBoard.Height / 8) * y);
                }

            foreach (checkerPiece piece in pieces)
            {
                piece.setSize(Convert.ToInt32(pnlBoard.Width / 8), Convert.ToInt32(pnlBoard.Width / 8));
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


        //Adds new piece to gameplay
        public void addPiece(checkerPiece.Team team, BoardTile tile)
        {
            checkerPiece piece = new checkerPiece(this, team);
            piece.move(tile);
            this.pieces.Add(piece);
        }

        //Adds new king to gameplay
        public void addKing(checkerPiece.Team team, BoardTile tile)
        {
            checkerPiece piece = new checkerPieceKing(this, team);
            piece.move(tile);
            this.getActivePlayer().setSelected(piece);
            this.pieces.Add(piece);
            this.redrawBoard();
        }

        //Method creates initial checkers pieces
        public void populateBoard()
        {
            pnlBoard.SuspendLayout();
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 4; x++)
                {
                    checkerPiece piece = new checkerPiece(this, checkerPiece.Team.WHITE);
                    piece.move(getTile(x*2+1, y));
                    if (y==1) //Offsetting by 1 pos on x-axis
                        piece.move(getTile(x*2, y));
                    pieces.Add(piece);
                }

            for (int y = 5; y < 8; y++)
                for (int x = 0; x < 4; x++)
                {
                    checkerPiece piece = new checkerPiece(this, checkerPiece.Team.DARK);
                    piece.move(getTile(x*2, y));
                    if(y==6) //Offsetting by 1 pos on x-axis
                        piece.move(getTile(x*2+1, y));
                    pieces.Add(piece);
                }

            this.redrawBoard();
            pnlBoard.ResumeLayout();
        }

        //Fired from click event on highlighted tile
        public void moveRequest(BoardTile tile)
        {
            bool hasJumped = false;
            if (this.getActivePlayer().getSelected().getJumpMoves().Contains(tile)) {   //Calculate the tile that was jumped over
                hasJumped = true;
                removeJumped(this.getActivePlayer().getSelected().getTile(), tile);
            }            
            this.getActivePlayer().getSelected().move(tile);
            this.clearHighlightedTiles();
            upgradeCheck(); //Check if piece should be upgraded to king

            if (hasJumped && this.getActivePlayer().getSelected().getJumpMoves().Count > 0) //If another jump avaliable after initial jump
            {
                this.getActivePlayer().getSelected().highlight();
                foreach (BoardTile jmpTile in this.getActivePlayer().getSelected().getJumpMoves())
                    jmpTile.highlight(true);
            }
            else {
                nextTurn();
            }
            
        }

        //Calculate and remove tile that was jumped over
        public void removeJumped(BoardTile originalTile, BoardTile newTile)
        {
            int jumpedX;
            int jumpedY;
            if (originalTile.getY() > newTile.getY())
            {
                jumpedY = newTile.getY() + 1;
            }
            else
            {
                jumpedY = newTile.getY() - 1;
            }
            if (originalTile.getX() > newTile.getX())
            {
                jumpedX = newTile.getX() + 1;
            }
            else
            {
                jumpedX = newTile.getX() - 1;
            }
            this.getCheckerPiece(this.getTile(jumpedX, jumpedY)).remove();
        }

        //Check if piece should be upgraded to king
        public void upgradeCheck()
        {
            if (this.getActivePlayer().getSelected().type == checkerPiece.Type.REGULAR) //Upgrade to king if reached end of board
                if (this.getActivePlayer().getSelected().getTile().getY() == 7 && this.getActivePlayer().getSelected().getTeam() == checkerPiece.Team.WHITE)
                {
                    this.getActivePlayer().getSelected().upgrade();
                }
                else if (this.getActivePlayer().getSelected().getTile().getY() == 0 && this.getActivePlayer().getSelected().getTeam() == checkerPiece.Team.DARK)
                {
                    this.getActivePlayer().getSelected().upgrade(); ;
                }
        }

        //Removes a checker piece from gameplay
        public void removePiece(checkerPiece piece)
        {
            pieces.Remove(piece);
            this.redrawBoard();
        }

        //Returns the current turn
        public checkerPiece.Team getTurn()
        {
            return turn;
        }


        //Returns the current player
        public Player getActivePlayer()
        {
            Player player = null;
            foreach (Player p in players)
            {
                if (p.getTeam() == turn) {
                    player = p;
                    break;
                }
            }
            return player;
        }


        //Clears highlighting on tiles
        public void clearHighlightedTiles()
        {
            foreach (BoardTile tile in board)
            {
                tile.highlight(false);
            }
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

        //Updates form title
        public void updateStatus(String status)
        {
            form.Text = "Checkers - " + status;
        }
    }
}

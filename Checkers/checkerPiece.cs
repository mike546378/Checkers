using Checkers.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    class checkerPiece
    {
        public enum Team
        {
            DARK = 1,
            WHITE = 2,
        }

        PictureBox piece = new PictureBox(); //The checker piece is represented by a pictureBox
        BoardManager manager; //Reference back to the boardmanager
        BoardTile location;
        Team team;

        //Initilization through constructor
        public checkerPiece(BoardManager man, checkerPiece.Team team)
        {
            manager = man;
            this.team = team;
            if (team == Team.DARK)
            {
                piece.BackgroundImage = Resources.DarkChecker;
            }
            else {
                piece.BackgroundImage = Resources.lightChecker;
            }
            piece.BackgroundImageLayout = ImageLayout.Stretch;
            piece.Click += new EventHandler(piece_Click);
            manager.getBoard().Controls.Add(piece);
            piece.BringToFront();
        }


        //Method to set size of the checker piece
        public void setSize(int width, int height)
        {
            piece.Size = new System.Drawing.Size(width,height);
        }


        //Highlights the piece indicating it can be moved
        public void highlight()
        {
            if (team == Team.DARK){
                piece.BackgroundImage = Resources.DarkChecker_Highlighted;
            }else {
                piece.BackgroundImage = Resources.lightChecker_Highlighted;
            }
        }


        //Clears the highlighting
        public void clearHighlight()
        {
            if (team == Team.DARK)
            {
                piece.BackgroundImage = Resources.DarkChecker1;
            }
            else
            {
                piece.BackgroundImage = Resources.lightChecker1;
            }
        }

        //Method retreives the team associated with the piece
        public Team getTeam()
        {
            return team;
        }

        //Method moves the checker piece
        public void move(BoardTile loc)
        {
            piece.Location = loc.Location;
            location = loc;
        }

        //Refreshes the location in event of resolution change
        public void updateLoc()
        {
            if(location != null)
                piece.Location = location.Location;
        }


        //Gets all avaliable moves for the piece
        private List<BoardTile> moves;
        public List<BoardTile> getMoves()
        {
            moves = new List<BoardTile>();
            BoardTile tile;
            if (team == Team.DARK)
            {
                checkMove(1, -1);  //Checking  ^>
                checkMove(-1, -1); //Checking <^         
            }
            else {
                checkMove(1, 1);  //Checking  \/>
                checkMove(-1, 1); //Checking <\/   
            }

            return moves;
        }

        //Checks for avaliable moves in specified x & y direction (THIS SHOULD ALWAYS BE +- 1)
        private void checkMove(int xDir, int yDir)
        {
            BoardTile tile;
            tile = manager.getTile(location.getX() + xDir, location.getY() + yDir);
            if (tile != null)
            {
                if (!manager.getTileContainsPiece(tile))
                {
                    moves.Add(manager.getTile(location.getX() + xDir, location.getY() + yDir)); //Tile Empty, able to move there
                }
                else
                {
                    if (manager.getCheckerPiece(tile).getTeam() != team) //Tile populated but contains enemy
                    {
                        if (manager.getTile(location.getX() + xDir*2, location.getY() + yDir*2) != null) //Tile past enemy exists
                            if (!manager.getTileContainsPiece(manager.getTile(location.getX() + xDir * 2, location.getY() + yDir * 2))) //Tile past enemy empty
                                moves.Add(manager.getTile(location.getX() + xDir * 2, location.getY() + yDir * 2));
                    }
                }

            }
        }

        //Event handler for clicking of piece
        public void piece_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click");
        }

        //Returns the tile that the piece is on
        public BoardTile getTile()
        {
            return location;
        }
    }
}

﻿using Checkers.Properties;
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

        protected PictureBox piece = new PictureBox(); //The checker piece is represented by a pictureBox
        BoardManager manager; //Reference back to the boardmanager
        BoardTile location;
        protected Team team;
        public bool highlighted;
        public Type type;
        public enum Type
        {
            REGULAR = 1,
            KING = 2,
        }

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
            this.type = Type.REGULAR;
            piece.BringToFront();
        }


        //Method to set size of the checker piece
        public void setSize(int width, int height)
        {
            piece.Size = new System.Drawing.Size(width,height);
        }


        //Highlights the piece indicating it can be moved
        virtual public void highlight()
        {
            if (team == Team.DARK){
                piece.BackgroundImage = Resources.DarkChecker_Highlighted;
            }else {
                piece.BackgroundImage = Resources.lightChecker_Highlighted;
            }
            highlighted = true;
        }


        //Clears the highlighting
        virtual public void clearHighlight()
        {
            if (team == Team.DARK)
            {
                piece.BackgroundImage = Resources.DarkChecker;
            }
            else
            {
               piece.BackgroundImage = Resources.lightChecker;
            }
            highlighted = false;
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

        //Removes piece from play
        public void remove()
        {
            if (manager.getForm().InvokeRequired)
            {
                manager.getForm().BeginInvoke(new Action(() => remove()));
                return;
            }
            piece.BackgroundImage = Resources.Tile_White;
            piece.Visible = false;
            piece.SendToBack();
            manager.removePiece(this);
        }

        //Gets all avaliable moves for the piece
        protected List<BoardTile> moves;
        protected List<BoardTile> jumpMoves;
        virtual public List<BoardTile> getMoves()
        {
            moves = new List<BoardTile>();
            jumpMoves = new List<BoardTile>();
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

        //Gets all avaliable jumpMoves for the piece
        public List<BoardTile> getJumpMoves()
        {
            getMoves();
            return jumpMoves;
        }

        //Checks for avaliable moves in specified x & y direction (THIS SHOULD ALWAYS BE +- 1)
        protected void checkMove(int xDir, int yDir)
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
                                jumpMoves.Add(manager.getTile(location.getX() + xDir * 2, location.getY() + yDir * 2));
                    }
                }

            }
        }

        //Event handler for clicking of piece
        public void piece_Click(object sender, EventArgs e)
        { 
            manager.clearHighlightedTiles();
            if (this.highlighted && this.getTeam() == manager.getTurn())
            {
                manager.getActivePlayer().setSelected(this);
                getMoves(); // Required to reset the jump moves
                List<BoardTile> moves = this.getJumpMoves();
                if (moves.Count == 0)
                    moves = this.getMoves();
                foreach (BoardTile tile in moves)
                    tile.highlight(true);
            }

        }

        //Returns the tile that the piece is on
        public BoardTile getTile()
        {
            return location;
        }


        //Returns the PictureBox of the piece
        public PictureBox getPiece()
        {
            return piece;
        }

        //Upgrades piece to a king
        public void upgrade()
        {
            manager.addKing(team, location);
            remove();
        }
    }
}

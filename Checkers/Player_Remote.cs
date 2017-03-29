using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Player_Remote : Player
    {

        Multiplayer mp;

        public Player_Remote(checkerPiece.Team team, BoardManager manager) : base(team, manager){ mp = new Multiplayer(this); this.playerType = PlayerType.Remote; }

        //Attempts to move a piece to a given location
        public Boolean move(int[] oldCoords, int[] newCoords) 
        {
            checkerPiece piece = manager.getCheckerPiece(manager.getTile(oldCoords[0], oldCoords[1]));
            if (piece.getTile().InvokeRequired)
            {
                piece.getTile().Invoke(new Action(() => move(oldCoords, newCoords)));
                return true;
            }
            piece.move(manager.getTile(newCoords[0], newCoords[1]));
            return true;
        }


        //Sends a move to the server
        public void sendMove(checkerPiece piece, BoardTile tile)    
        {
            mp.send("relay=move="+piece.getTile().getX()+","+ piece.getTile().getY()+"|"+tile.getX()+","+tile.getY());
        }

        //Sends a delete message to the server
        public void sendDel(checkerPiece piece)
        {
            mp.send("relay=del=" + piece.getTile().getX() + "," + piece.getTile().getY());
        }

        //Sends a upgrade message to the server
        public void sendUpgrade(checkerPiece piece)
        {
            mp.send("relay=king=" + piece.getTile().getX() + "," + piece.getTile().getY());
        }

        //Starts turn
        public override void startTurn(){
            this.clearHighlights();
            mp.send("relay=nextturn");
        }

        //Ends game
        public void sendEnd(Boolean won)
        {
            won = !won;
            mp.send("relay=end=" + won);
        }

        //Terminates connection to server
        public void terminateConnection()
        {
            mp.closeConnection();
        }
    }
}

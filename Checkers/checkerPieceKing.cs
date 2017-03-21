using Checkers.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class checkerPieceKing : checkerPiece
    {
        public checkerPieceKing(BoardManager man, Team team) : base(man, team){
            if (team == Team.DARK)
            {
                getPiece().BackgroundImage = Resources.DarkCheckerKing1;
            }
            else
            {
                getPiece().BackgroundImage = Resources.lightCheckerKing1;
            }
            this.type = Type.KING;
        }

        //Returns avaliable moves
        override public List<BoardTile> getMoves()
        {
            moves = new List<BoardTile>();
            jumpMoves = new List<BoardTile>();
            checkMove(1, -1);  //Checking  ^>
            checkMove(-1, -1); //Checking <^
            checkMove(1, 1);  //Checking  \/>
            checkMove(-1, 1); //Checking <\/   
            return moves;
        }

        //Highlights the piece indicating it can be moved
        override public void highlight()
        {
            if (team == Team.DARK)
            {
                piece.BackgroundImage = Resources.DarkCheckerKing_Highlighted;
            }
            else
            {
                piece.BackgroundImage = Resources.lightCheckerKing_Highlighted;
            }
            highlighted = true;
        }


        //Clears the highlighting
        override public void clearHighlight()
        {
            if (team == Team.DARK)
            {
                piece.BackgroundImage = Resources.DarkCheckerKing;
            }
            else
            {
                piece.BackgroundImage = Resources.lightCheckerKing;
            }
            highlighted = false;
        }
    }
}

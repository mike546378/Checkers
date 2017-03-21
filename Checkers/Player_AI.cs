using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Player_AI : Player
    {
        public Player_AI(checkerPiece.Team team, BoardManager manager) : base(team, manager){ this.playerType = PlayerType.AI; }

        public override void startTurn()
        {

            while (true)
            {
                List<checkerPiece> avaliableMoves = new List<checkerPiece>();       //Pieces with avaliable regular moves
                List<checkerPiece> avaliableJumpMoves = new List<checkerPiece>();   //Pieces with avaliable jump moves

                //Retrieve all pieces with avaliable moves and jumpMoves
                foreach (checkerPiece piece in manager.getAllPieces(team))
                {
                    if (piece.getMoves().Count > 0)
                        avaliableMoves.Add(piece);
                    if (piece.getJumpMoves().Count > 0)
                        avaliableJumpMoves.Add(piece);
                }



                //Try to do a jump move if avaliable
                if (avaliableJumpMoves.Count > 0)
                {
                    this.selectedPiece = randomPiece(avaliableJumpMoves);
                }
                else
                {
                    this.selectedPiece = randomPiece(avaliableMoves);
                }


                //Select random move from whats avaliable
                if (this.selectedPiece.getJumpMoves().Count > 0)
                {
                    BoardTile newTile = randomMove(selectedPiece.getJumpMoves());
                    manager.removeJumped(selectedPiece.getTile(), newTile);
                    move(selectedPiece, newTile);
                    manager.upgradeCheck();

                    if (selectedPiece.getJumpMoves().Count == 0)
                        break;
                }
                else
                {
                    move(selectedPiece, randomMove(selectedPiece.getMoves()));
                    manager.upgradeCheck();
                    break;
                }
            }
            endTurn();
        }

        //Select random tile to move to from given list
        private BoardTile randomMove(List<BoardTile> moves)
        {
            Random rnd = new Random();
            return moves[rnd.Next(0, moves.Count - 1)];
        }

        //Select random piece from avaliable pieces
        private checkerPiece randomPiece(List<checkerPiece> pieceList)
        {
            Random rnd = new Random();
            return pieceList[rnd.Next(0,pieceList.Count-1)];
        }
    }
}

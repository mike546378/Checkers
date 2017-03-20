using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Player
    {
        private checkerPiece.Team team;
        private PlayerType playerType;
        private BoardManager manager;

        public enum PlayerType
        {
            Human = 1,
            AI = 2,
            Remote = 3,
        }

        public Player(checkerPiece.Team t, BoardManager m)
        {
            team = t;
            playerType = PlayerType.Human;
            manager = m;
            highlightAvaliableMoves();
        }

        public void highlightAvaliableMoves()
        {
            List<BoardTile> avaliableMoves = new List<BoardTile>();
            foreach (checkerPiece piece in manager.getAllPieces(team))
                if (piece.getMoves().Count > 0)
                    piece.highlight();
        }

        public Boolean move(checkerPiece piece, BoardTile location)
        {
            if (piece.getTeam() != team || location == null)
                return false;
            piece.move(location);
            return true;
        }
    }
}

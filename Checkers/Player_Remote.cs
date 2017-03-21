using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Player_Remote : Player
    {
        public Player_Remote(checkerPiece.Team team, BoardManager manager, PlayerType playerType) : base(team, manager, playerType){}
    }
}

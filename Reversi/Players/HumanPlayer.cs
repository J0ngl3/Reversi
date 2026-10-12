using Reversi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Players
{
    public class HumanPlayer : Player
    {
        public override bool IsHuman => true;

        public override Dictionary<Position, List<Position>> MakeMove(Dictionary<Position, List<Position>> moves)
        {

        }
    }
}

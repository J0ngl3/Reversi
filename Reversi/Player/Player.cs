using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reversi.Model;

namespace Reversi.Player
{
    public abstract class Player
    {
        public abstract bool IsHuman { get; }

        public abstract Dictionary<Position, List<Position>> MakeMove(Dictionary<Position, List<Position>> moves);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reversi.Model;

namespace Reversi.Players
{
    public abstract class Player
    {
        public abstract bool IsHuman { get; }

        public abstract Task<Dictionary<Position, List<Position>>> MakeMove(Dictionary<Position, List<Position>> moves);
    }
}

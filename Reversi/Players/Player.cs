using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reversi.Model;

namespace Reversi.Players
{
    /// <summary>
    /// Abstract class for player. is allowed to recieve value.
    /// </summary>
    public abstract class Player
    {
        public string Name { get; set; }
        public abstract bool IsHuman { get; }

        public abstract Task<Dictionary<Position, List<Position>>> MakeMove(Dictionary<Position, List<Position>> moves);
    }
}

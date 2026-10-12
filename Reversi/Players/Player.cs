using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Players
{
    /// <summary>
    /// Abstract class for player. is allowed to recieve value.
    /// </summary>
    public abstract class Player
    {
        public string Name { get; set; }
        public abstract bool IsHuman { get; }
    }
}

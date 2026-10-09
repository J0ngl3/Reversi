using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Players
{
    public abstract class Player
    {
        public abstract bool IsHuman { get; }
    }
}

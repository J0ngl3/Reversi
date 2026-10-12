using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Players
{
    /// <summary>
    /// boolean condition. Sets the value to true when player 1 or 2 is human.
    /// </summary>
    public class HumanPlayer : Player
    {
        public override bool IsHuman => true;
    }
}

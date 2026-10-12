using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Players
{
    /// <summary>
    /// boolean condition. Sets the value to false when the player 1 or 2 is CPU.
    /// </summary>
    public class ComputerPlayer : Player
    {
        public override bool IsHuman => false;
    }
}

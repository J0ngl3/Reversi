using Reversi.Model;
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
        // public override bool IsHuman => false;
        public override Task<KeyValuePair<Position, List<Position>>> MakeMove(Dictionary<Position, List<Position>> moves)
        {
            if (moves.Count < 1)
            {
                AbleToMakeMove = false;
                return Task.FromResult(new KeyValuePair<Position, List<Position>>());
            }

            List<Position> legalmoves = moves.Keys.ToList();
            int randomindex = Random.Shared.Next(legalmoves.Count);
            Position chosenmove = legalmoves[randomindex];

            var selectedmove = new Dictionary<Position, List<Position>>
            {
                {chosenmove, moves[chosenmove] }
            };

            return Task.FromResult(selectedmove);
        }
    }
}

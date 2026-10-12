using Reversi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reversi.Model;

namespace Reversi.Player
{
    public class ComputerPlayer : Player
    {
        public override bool IsHuman => false;

        public override Task<Dictionary<Position, List<Position>>> MakeMove(Dictionary<Position, List<Position>> moves)
        {
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

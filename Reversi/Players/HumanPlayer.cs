using Reversi.Model;
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
        // public override bool IsHuman => true;
        private Dictionary<Position, List<Position>> LegalMoves;
        private TaskCompletionSource<Dictionary<Position, List<Position>>> MoveCompletion;

        public override Task<KeyValuePair<Position, List<Position>>> MakeMove(Dictionary<Position, List<Position>> moves)
        {
            if (moves.Count < 1)
            {
                AbleToMakeMove = false;
                return Task.FromResult(new KeyValuePair<Position, List<Position>>());
            }

            LegalMoves = moves;
            MoveCompletion = new TaskCompletionSource<Dictionary<Position, List<Position>>>();
            return MoveCompletion.Task;
        }

        public bool TrySelectMove(Position position)
        {
            if (LegalMoves != null && MoveCompletion != null && LegalMoves.TryGetValue(position, out var outflanked))
            {
                var selectedMove = new Dictionary<Position, List<Position>>
                {
                    {position,outflanked }
                };

                MoveCompletion.TrySetResult(selectedMove);
                LegalMoves = null;
                MoveCompletion = null;
                return true;
            }

            return false;
        }
    }
}

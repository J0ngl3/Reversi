using Reversi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Players
{
    public class HumanPlayer : Player
    {
        public override bool IsHuman => true;

        private Dictionary<Position, List<Position>> LegalMoves;
        private TaskCompletionSource<Dictionary<Position, List<Position>>> MoveCompletion;

        public override Task<Dictionary<Position, List<Position>>> MakeMove(Dictionary<Position, List<Position>> moves)
        {
            LegalMoves = moves;
            MoveCompletion = new TaskCompletionSource<Dictionary<Position, List<Position>>>
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

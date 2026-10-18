using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reversi.Game;
using Reversi.Model;

namespace Reversi.Players
{
    /// <summary>
    /// Humanplayer subclass of superclass Player
    /// </summary>
    public class HumanPlayer : Player
    {
        // dictionary of all possible legal moves the player could make
        private Dictionary<Position, List<Position>> LegalMoves;
        private TaskCompletionSource<KeyValuePair<Position, List<Position>>> MoveCompletion;

        /// <summary>
        /// MakeMove method that allows the user to "request" a certain legal move to be made.
        /// </summary>
        /// <param name="moves"> the valid moves the user could make</param>
        /// <returns> a task that contains the move the user wants to make</returns>
        public override Task<KeyValuePair<Position, List<Position>>> MakeMove(GameBoard boardCopy, Dictionary<Position, List<Position>> moves)
        {
            // if the user doesn't have any valid moves, return a task with an empty key value pair
            if (!moves.Any())
            {
                AbleToMakeMove = false;
                return Task.FromResult(new KeyValuePair<Position, List<Position>>());
            }

            AbleToMakeMove = true;
            LegalMoves = moves;
            MoveCompletion = new TaskCompletionSource<KeyValuePair<Position, List<Position>>>();
            return MoveCompletion.Task;
        }

        /// <summary>
        /// Method that allows the user to select a move from the legal moves
        /// If the move is valid, it will complete the task returned by MakeMove.
        /// </summary>
        /// <param name="position"> the position the user wants to make a move at</param>
        /// <returns> true if the move was successfully selected, otherwise false</returns>
        public bool TrySelectMove(Position position)
        {
            if (LegalMoves != null && MoveCompletion != null && LegalMoves.TryGetValue(position, out var outflanked))
            {
                var selectedMove = new KeyValuePair<Position, List<Position>>(position, outflanked);
                

                MoveCompletion.TrySetResult(selectedMove);
                LegalMoves = null;
                MoveCompletion = null;
                return true;
            }

            return false;
        }
    }
}

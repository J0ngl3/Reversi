using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Reversi.Game;
using Reversi.Model;

namespace Reversi.Players
{
    /// <summary>
    /// Computerplayer subclass of superclass Player
    /// </summary>    
    public class ComputerPlayer : Player
    {
        // random object to generate random numbers for selecting a move
        private static readonly Random random = new Random();

        /// <summary>
        /// MakeMove method that allows the computer to "request" a certain legal move to be made.
        /// </summary>
        /// <param name="moves"> the valid moves the computer could make</param>
        /// <returns> a task that contains the move the user wants to make</returns>
        public override Task<KeyValuePair<Position, List<Position>>> MakeMove(GameBoard boardCopy, Dictionary<Position, List<Position>> moves)
        {
            // if the computer doesn't have any valid moves, return a task with an empty key value pair
            if (!moves.Any())
            {
                AbleToMakeMove = false;
                return Task.FromResult(new KeyValuePair<Position, List<Position>>());

            }

            AbleToMakeMove = true;
            return Task.Run(() =>
            {
                Thread.Sleep(50);
                List<Position> legalmoves = moves.Keys.ToList();
                int randomindex = random.Next(legalmoves.Count);
                Position chosenmove = legalmoves[randomindex];

                var selectedmove = new KeyValuePair<Position, List<Position>>
                (chosenmove, moves[chosenmove]);

                return selectedmove;
            });
            

        }
    }
}

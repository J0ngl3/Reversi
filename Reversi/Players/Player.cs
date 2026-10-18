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
    /// Abstract class for player
    /// </summary>
    public abstract class Player
    {
        public string Name { get; set; }
        public PlayerColor playerColor { get; set; }
        public bool AbleToMakeMove { get; set;}

        /// <summary>
        /// Method for player to make a move, returns the position of the move and the list of positions that will be flipped
        /// </summary>
        /// <param name="moves"> the valid moves for the player</param>
        /// <returns></returns>
        public abstract Task<KeyValuePair<Position, List<Position>>> MakeMove(GameBoard boardCopy, Dictionary<Position, List<Position>> moves);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Model
{
    /// <summary>
    /// Represents the color of a disc on the board
    /// If none, the position is empty
    /// otherwise black or white.
    /// </summary>
    public enum PlayerColor
    {
        None, White, Black
    }

    /// <summary>
    /// An extension method that allows us to easily get the opponents color
    /// </summary>
    public static class PlayerColorExtension
    {
        public static PlayerColor Opponent (this PlayerColor player)
        {
            if (player == PlayerColor.Black)
            {
                return PlayerColor.White;
            }   
            else if (player == PlayerColor.White)
            {
                return PlayerColor.Black;
            }

            // If the player is None, then there is no opponent so it returns None
            return PlayerColor.None;
        }
    }
}

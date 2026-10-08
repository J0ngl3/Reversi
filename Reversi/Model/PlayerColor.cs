using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Model
{
    public enum PlayerColor
    {
        None, White, Black
    }

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

            return PlayerColor.None;
        }
    }
}

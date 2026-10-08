using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Reversi.Model;

namespace Reversi.Game
{
    internal class GameBoard
    {
        public const int Rows = 8;
        public const int Cols = 8;

        public PlayerColor[,] Board { get; }

        public Dictionary<PlayerColor, int> DiscCount { get; }
        public PlayerColor CurrentPlayer;

        /// <summary>
        /// Applies the cross pattern for the four starting discs and sets
        /// start player to be black
        /// </summary>
        public GameBoard()
        {
            Board = new PlayerColor[Rows, Cols];

            Board[3, 3] = PlayerColor.White;
            Board[3, 4] = PlayerColor.Black;
            Board[4, 3] = PlayerColor.Black;
            Board[4, 4] = PlayerColor.White;

            DiscCount = new Dictionary<PlayerColor, int>()
            {
                { PlayerColor.White, 2},
                { PlayerColor.Black, 2}
            };

            CurrentPlayer = PlayerColor.Black;

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Reversi.Game
{
    internal class GameBoard
    {
        public const int Rows = 8;
        public const int Cols = 8;

        public Color[,] Board { get; };

        public Dictionary<Color, int> DiscCount { get; };

    }
}

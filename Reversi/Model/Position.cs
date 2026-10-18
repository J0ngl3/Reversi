using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Reversi.Model
{
    /// <summary>
    /// Represents a position on the board
    /// with row and column coordinates
    /// the first row and first column start at 0,0
    /// </summary>
    public class Position
    {
        public int Row { get; }
        public int Col { get; }

        /// <summary>
        /// Constructor that initializes the row and column coordinates
        /// </summary>
        /// <param name="row"> The row coordinate</param>
        /// <param name="col">The column coordinate</param>
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}

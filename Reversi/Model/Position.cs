using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Reversi.Model
{
    public class Position
    {
        public int Row { get; }
        public int Col { get; }


        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}

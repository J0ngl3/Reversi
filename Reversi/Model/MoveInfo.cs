using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Model
{
    public class MoveInfo
    {
        public Color Color { get; set; }
        public Position Pos { get; set; }
        public List<Position> Outflanked { get; set; };

    }
}

using Reversi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Reversi.Game
{
    public class GameManager
    {
        public Color CurrentPlayer { get; private set;}
        public bool GameOver { get; private set; }
        public Color Winner { get; private set; }
        public Dictionary<Position, List<Position>> ValidMoves { get; private set; }




    }
}

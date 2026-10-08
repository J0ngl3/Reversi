using Reversi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Reversi.Player;

namespace Reversi.Game
{
    public class GameManager
    {
        public Color CurrentPlayer { get; private set;}
        public bool GameOver { get; private set; }
        public Color Winner { get; private set; }
        public Dictionary<Position, List<Position>> ValidMoves { get; private set; }

        // here I think Mr.Mahdy must do some stuff.
        // we gotta implement some code that is like
        // PRESS THE (HvH) button, both players
        public GameBoard Board = new GameBoard();
        Player UserOne = new Player();
        public Player PlayerChoice(Player One, Player Two)
        {

        }






    }
}

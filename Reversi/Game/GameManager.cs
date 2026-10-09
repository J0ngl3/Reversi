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
            
        //On it. //Mahdy

        public GameMode Mode { get; private set; }
        public GameManager(GameMode mode)
        {
            Mode = mode;
            switch(mode)
            {
                case GameMode.hvh:
                    UserOne = new HumanPlayer();
                    UserTwo = new HumanPlayer();
                    break;
                case GameMode.hvc:
                    UserOne = new HumanPlayer();
                    UserTwo = new ComputerPlayer();
                    break;
                case GameMode.cvc:
                    UserOne = new ComputerPlayer();
                    UserTwo = new ComputerPlayer();
                    break;
            }
        }
        public GameBoard Board = new GameBoard();
        Player UserOne = new Player();
        Player UserTwo = new Player();
        public Player PlayerChoice(Player One, Player Two)
        {

        }






    }
}
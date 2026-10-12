using Reversi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Reversi.Players;

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
        public Player UserOne { get; private set; }
        public Player UserTwo { get; private set; }
        public GameBoard Board = new GameBoard();
        public GameManager(GameMode mode, string name1, string name2, bool human1, bool human2)
        {
            Mode = mode;
            if (human1)
            {
                UserOne = new HumanPlayer();
            }
            else
            {
                UserOne = new ComputerPlayer();
            }
            if (human2)
            {
                UserTwo = new HumanPlayer();
            }
            else
            {
                UserTwo= new ComputerPlayer();
            }
            UserOne.Name = name1;
            UserTwo.Name = name2;
        }
        
         
        /*public Player PlayerChoice(Player One, Player Two)
        {

        }*/
    }
}
using Reversi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Reversi.Players;
using System.Security.Cryptography.X509Certificates;

namespace Reversi.Game
{
    public class GameManager
    {
        public Player CurrentUser { get; private set; }
        public Player OtherUser { get; private set; }
        public bool GameOver { get; private set; }
        public PlayerColor Winner { get; private set; }
        public bool Tie { get; private set; }
        public Dictionary<Position, List<Position>> ValidMoves { get; private set; }
        public ValidMovesCalc validMovesCalc = new ValidMovesCalc();
        public event Action BoardRefreshed;

        // here I think Mr.Mahdy must do some stuff.

        //On it. //Mahdy

        public GameMode Mode { get; private set; }
        public Player UserOne { get; private set; }
        public Player UserTwo { get; private set; }
        public GameBoard gameBoard = new GameBoard();
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
                UserTwo = new ComputerPlayer();
            }
            UserOne.Name = name1;
            UserTwo.Name = name2;

            UserOne.playerColor = PlayerColor.Black;
            UserTwo.playerColor = PlayerColor.White;
            
            CurrentUser = UserOne;
            OtherUser = UserTwo;
        }

        public async Task GameStarted()
        {
            while (true)
            {
                Dictionary<Position, List< Position >> validMoves = validMovesCalc.GetValidMoves(CurrentUser.playerColor, gameBoard.Board);
                if (!validMoves.Any())
                {
                    CurrentUser.AbleToMakeMove = false;
                    if (!OtherUser.AbleToMakeMove)
                    {
                        GameOver = true;
                        if(gameBoard.DiscCount[PlayerColor.White] == gameBoard.DiscCount[PlayerColor.Black])
                        {
                            Tie = true;
                            // send into to window that game is a tie and over
                        }
                        else if (gameBoard.DiscCount[PlayerColor.White] > gameBoard.DiscCount[PlayerColor.Black])
                        {
                            Winner = PlayerColor.White;
                        }
                        else
                        {
                            Winner = PlayerColor.Black;
                        }

                        // make sure the winner window is displayed
                    }
                    break;
                }    

                KeyValuePair<Position, List<Position>> UserMove = 
                    (await CurrentUser.MakeMove(validMoves));
                gameBoard.ApplyMove(UserMove.Key, CurrentUser.playerColor, UserMove.Value);

                if (CurrentUser == UserOne)
                {
                    CurrentUser = UserTwo;
                    OtherUser = UserOne;
                }
                else
                {
                    CurrentUser = UserTwo;
                    OtherUser = UserOne;
                }

            }
        }
    }
}
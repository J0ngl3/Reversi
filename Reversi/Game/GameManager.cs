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
    /// <summary>
    /// Represents the general manager of the game, includes the players, game board, controlling move turns, determines the winner,
    /// handles the interaction between players and game board, and raises events to the UI
    /// </summary>
    public class GameManager
    {
        // all the properties and events
        public event Action BoardRefreshed;
        public Player CurrentUser { get; private set; }
        public Player OtherUser { get; private set; }
        public bool GameOver { get; private set; }
        public PlayerColor Winner { get; private set; }
        public Dictionary<Position, List<Position>> ValidMoves { get; private set; }
        public ValidMovesCalc validMovesCalc = new ValidMovesCalc();
        public event Action GameFinished;


        // the two players, either human or computer
        public Player UserOne { get; private set; }
        public Player UserTwo { get; private set; }
        // the game board itself
        public GameBoard gameBoard = new GameBoard();

        /// <summary>
        /// Constructor for the class, takes in names of the players and whether they are human or computer.
        /// </summary>
        /// <param name="name1">name of the first player</param>
        /// <param name="name2">name of the second player</param>
        /// <param name="human1">human or computer bool for userOne</param>
        /// <param name="human2">human or computer bool for userTwo</param>
        public GameManager(string name1, string name2, bool human1, bool human2)
        {
            // UserOne is always black and goes first
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

        /// <summary>
        /// Swaps the current and other user, called after a move is made or if a player has no valid moves
        /// </summary>
        public void PassTurn()
        {
            if (CurrentUser == UserOne)
            {
                CurrentUser = UserTwo;
                OtherUser = UserOne;
            }
            else
            {
                CurrentUser = UserOne;
                OtherUser = UserTwo;
            }
        }

        /// <summary>
        /// the main game loop, runs game until no valid moves are available for either player,
        /// then determines winner and raises GameFinished event to the UI
        /// </summary>
        /// <returns> task that can be awaited so the UI can remain responsive </returns>
        public async Task GameStarted()
        {
            while (true)
            {
                // creates a new task that allows the UI to not freeze when calculating valid moves
                Task task = Task.Run(() =>
                {
                    ValidMoves = validMovesCalc.GetValidMoves(CurrentUser.playerColor, gameBoard.Board);
                });
                await task;
                // refreshes the UI board to reflect where the user can make a move
                BoardRefreshed?.Invoke();

                // check if the user can make a move, and determine whether the game is over.
                if (!ValidMoves.Any())
                {
                    // set the bool
                    CurrentUser.AbleToMakeMove = false;
                    if(!validMovesCalc.GetValidMoves(OtherUser.playerColor, gameBoard.Board).Any())
                    {
                        GameOver = true;
                        Winner = gameBoard.InTheLead();
                        // if NONE, it's a tie, and report that. Otherwise, display the color that won
                        // make sure the winner window is displayed
                        // end the game here
                        break;
                    }
                    PassTurn();
                    continue;
                }
                CurrentUser.AbleToMakeMove = true;
                KeyValuePair<Position, List<Position>> UserMove = 
                    (await CurrentUser.MakeMove(ValidMoves));
                gameBoard.ApplyMove(UserMove.Key, CurrentUser.playerColor, UserMove.Value);
                
                // create an event that is sent to XAML and mahdy.
                PassTurn();
                
            }
            GameFinished?.Invoke();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Reversi.Model;
using System.Threading;

namespace Reversi.Game
{
    /// <summary>
    /// Represents the game board, the state of the board, disc counts for each player, methods to apply moves and what player is in the lead
    /// </summary>
    public class GameBoard
    {
        // The amount of rows and columns
        public const int Rows = 8;
        public const int Cols = 8;

        // 2D array to represent the board, either none, white or black filled
        public PlayerColor[,] Board { get; }

        // Dictionary to keep track of the disc count for each player
        public Dictionary<PlayerColor, int> DiscCount { get; }

        /// <summary>
        /// Applies the cross pattern for the four starting discs and sets
        /// start player to be black. Also instantiates the disc count with each player set to 2 discs
        /// </summary>
        public GameBoard()
        {
            Board = new PlayerColor[Rows, Cols];

            Board[3, 3] = PlayerColor.White;
            Board[3, 4] = PlayerColor.Black;
            Board[4, 3] = PlayerColor.Black;
            Board[4, 4] = PlayerColor.White;

            DiscCount = new Dictionary<PlayerColor, int>()
            {
                { PlayerColor.White, 2},
                { PlayerColor.Black, 2}
            };
        }

        /// <summary>
        /// A copy constructor that creates a new "copy" instance of the game board
        /// used for the players to simulate moves without changing the actual game board
        /// </summary>
        /// <param name="board"> the original game board to copy</param>
        public GameBoard(GameBoard other)
        {
            Board = (PlayerColor[,])other.Board.Clone();
            DiscCount = new Dictionary<PlayerColor, int>(other.DiscCount);
        }

        /// <summary>
        /// Applies a move to the board called from GameManager
        /// Flips over potential discs and adds the new disc, updates the disc count
        /// </summary>
        /// <param name="pos"> the position on the board where the new disc is placed</param>
        /// <param name="player"> the color of the player making the move</param>
        /// <param name="outflanked"> list of all the positions that will be flipped</param>
        public void ApplyMove(Position pos, PlayerColor player, List<Position> outflanked)
        {
            foreach (Position disc in outflanked)
            {
                Board[disc.Row, disc.Col] = player;
                //decrease opponent's count when disk is flipped, and increment current player's
                DiscCount[player.Opponent()] -= 1; 
                DiscCount[player] += 1;
            }
            // lastly add the new disc and add that to the count
            Board[pos.Row, pos.Col] = player;
            DiscCount[player] += 1;
        }

        /// <summary>
        /// Returns the player color that is currently in the lead based on disc count.
        /// </summary>
        /// <returns> returns a color, if None, the game is currently tied</returns>
        public PlayerColor InTheLead()
        {
            if (DiscCount[PlayerColor.White] == DiscCount[PlayerColor.Black])
            {
                return PlayerColor.None;
            }
            else if (DiscCount[PlayerColor.White] > DiscCount[PlayerColor.Black])
            {
                return PlayerColor.White;
            }
            else
            {
                return PlayerColor.Black;
            }
        }
    }
}

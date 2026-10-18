using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reversi.Game;

namespace Reversi.Model
{
    /// <summary>
    /// A calculator class that is used to determine the valid moves for a player given the current state of the board
    /// </summary>
    public class ValidMovesCalc
    {
        // the amount of rows and columns
        public int Rows = 8;
        public int Cols = 8;
        // a dictionary that stores the valid moves for a player, along with the corresponding "outflanked" positions that would be flipped
        private Dictionary<Position, List<Position>> ValidMoves;

        /// <summary>
        /// Boolean method that checks if a given position is inside the board boundaries
        /// </summary>
        /// <param name="r"> the row coordinate</param>
        /// <param name="c"> the column coordinate</param>
        /// <returns> true if the position is inside the board, otherwise false</returns>
        private bool isInsideBoard(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Cols;
        }

        /// <summary>
        /// Boolean method that checks if a given move is legal for a player
        /// </summary>
        /// <param name="player"> the color of the player</param>
        /// <param name="pos"> the position of the move</param>
        /// <param name="board"> the game board the game is played on</param>
        /// <param name="outflanked"> list of positions that would be flipped by the move</param>
        /// <returns> true if the move is legal, otherwise false</returns>
        private bool IsMoveLegal(PlayerColor player, Position pos, PlayerColor[,] board, List<Position> outflanked)
        {
            if (board[pos.Row, pos.Col] != PlayerColor.None)
            {
                outflanked = null;
                return false;
            }

            return outflanked.Count > 0;
        }

        /// <summary>
        /// Main "GetValidMoves" method that calculates all the different valid moves for a player
        /// </summary>
        /// <param name="player"> the color of the player</param>
        /// <param name="board"> the game board the game is played on</param>
        /// <returns> a dictionary of valid moves and their corresponding outflanked positions that would be flipped</returns>
        public Dictionary<Position, List<Position>> GetValidMoves(PlayerColor player, PlayerColor[,] board)
        {
            ValidMoves = new Dictionary<Position, List<Position>>();
            // double for loop to iterate through every cell on the board
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    Position pos = new Position(r, c);
                    if (IsMoveLegal(player, pos, board, OutFlankedEverywhere(pos, player, board)))
                    {
                        ValidMoves.Add((pos),OutFlankedEverywhere(pos, player, board));
                    }
                }
            }
            return ValidMoves;
        }

        /// <summary>
        /// Method that calculates the outflanked positions in a given direction for a move
        /// </summary>
        /// <param name="pos"> the position of the move</param>
        /// <param name="player"> the color of the player</param>
        /// <param name="board"> the game board the game is played on</param>
        /// <param name="rDelta"> the row direction to check</param>
        /// <param name="cDelta"> the column direction to check</param>
        /// <returns> a list of positions that would be flipped</returns>
        public List<Position> OutFlankedInDirection(Position pos, PlayerColor player, PlayerColor[,] board, int rDelta, int cDelta)
        {
            List<Position> outFlanked = new List<Position>();
            int r = pos.Row + rDelta;
            int c = pos.Col + cDelta;
            while(isInsideBoard(r,c) && board[r,c] != PlayerColor.None)
            {
                if (board[r,c] == player.Opponent())
                {
                    outFlanked.Add(new Position(r, c));
                    r += rDelta;
                    c += cDelta;
                }
                else
                {
                    return outFlanked;
                }
            }
            return new List<Position>();
        }

        /// <summary>
        /// Method that calculates the outflanked positions in all directions for a move
        /// works by using previous "OutFlankedInDirection" method in a double for loop to check all 8 directions
        /// </summary>
        /// <param name="pos"> the position of the move</param>
        /// <param name="player"> the color of the player</param>
        /// <param name="board"> the game board the game is played on</param>
        /// <returns> a list of positions that would be flipped</returns>
        public List<Position> OutFlankedEverywhere(Position pos, PlayerColor player, PlayerColor[,] board)
        {
            List<Position> outFlanked = new List<Position>();
            for (int rDelta = -1; rDelta <= 1; rDelta++)
            {
                for(int cDelta = -1; cDelta <= 1; cDelta ++)
                {
                    if(rDelta == 0 && cDelta == 0)
                    {
                        continue;
                    }
                    
                    outFlanked.AddRange(OutFlankedInDirection(pos, player, board, rDelta, cDelta));
                }
            }
            return outFlanked;
        }
        
    }
}
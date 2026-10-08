using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reversi.Game;

namespace Reversi.Model
{
    internal class ValidMovesCalc
    {
        public int Rows = 8;
        public int Cols = 8;
        private Dictionary<Position, List<Position>> ValidMoves;
        // private List<Position> OutFlanked;
        

        private bool isInsideBoard(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Cols;
        }

        private bool IsMoveLegal(PlayerColor player, Position pos, PlayerColor[,] board, List<Position> outflanked)
        {
            if (board[pos.Row, pos.Col] != PlayerColor.None)
            {
                outflanked = null;
                return false;
            }

            return outflanked.Count > 0;
        }

        public Dictionary<Position, List<Position>> GetValidMoves(PlayerColor player, PlayerColor[,] board)
        {
            ValidMoves = new Dictionary<Position, List<Position>>();
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

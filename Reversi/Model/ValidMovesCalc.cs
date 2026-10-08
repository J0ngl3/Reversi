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
        private List<Position> OutFlanked;
        

        private bool isInsideBoard(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Cols;
        }



        public Dictionary<Position, List<Position>> GetValidMoves(PlayerColor player, PlayerColor[,] board)
        {
            
            ValidMoves = new Dictionary<Position, List<Position>>();
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    Position pos = new Position(r, c);
                    if (isMoveLegal(OutFlankedEverywhere(pos, player, board)))
                    {
                        ValidMoves.Add((pos),OutFlankedEverywhere(pos, player, board));
                    }
                }
            }


        }

        public List<Position> OutFlankedInDirection(Position pos, PlayerColor player, PlayerColor[,] board, int rDelta, int cDelta)
        {
            OutFlanked = new List<Position>();
            int r = pos.Row + rDelta;
            int c = pos.Col + cDelta;
            while(isInsideBoard(r,c) && board[r,c] != PlayerColor.None)
            {
                if (board[r,c] == player.Opponent())
                {
                    OutFlanked.Add(new Position(r, c));
                    r += rDelta;
                    c += cDelta;
                }
                else
                {
                    return OutFlanked;
                }
            }
            return new List<Position>();
        }

        public List<Position> OutFlankedEverywhere(Position pos, PlayerColor player, PlayerColor[,] board)
        {
            List<Position> OutFlanked = new List<Position>();
            for (int rDelta = -1; rDelta <= 1; rDelta++)
            {
                for(int cDelta = -1; cDelta <= 1; cDelta ++)
                {
                    if(rDelta == 0 && cDelta == 0)
                    {
                        continue;
                    }
                    
                    OutFlanked.AddRange(OutFlankedInDirection(pos, player, board, rDelta, cDelta));
                }
            }
            return OutFlanked;
        }
        
        private bool IsMoveLegal(PlayerColor player, Position pos, PlayerColor[,] board, out List<Position> outflanked)
        {
            if (board[pos.Row, pos.Col] != PlayerColor.None)
            {
                outflanked == null;
                return false;
            }

            outflanked
        }
    }
}

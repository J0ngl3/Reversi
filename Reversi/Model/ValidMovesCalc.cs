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
        public Dictionary<PlayerColor, List<PlayerColor>> ValidMoves;
        private List<Position> OutFlanked;
        

        private bool isInsideBoard(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Cols;
        }



        public Dictionary<Position, List<Position>> GetValidMoves(PlayerColor[,] board, PlayerColor currentPlayer)
        {
            
                = OutFlankedInDirection()
            ValidMoves = new Dictionary<Position, List<Position>>()

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
        }
        

    }
}

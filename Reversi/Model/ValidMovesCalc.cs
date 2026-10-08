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



        public void GetValidMoves(PlayerColor[,] board, PlayerColor currentPlayer)
        {
            OutFlanked = new List<Position>();
            public PlayerColor Opponent { get {} }
            

        }

        

    }
}

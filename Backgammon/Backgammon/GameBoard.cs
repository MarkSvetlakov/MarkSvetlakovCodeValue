using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    class GameBoard
    {

        private Field[] _fields;

        public GameBoard()
        {
            _fields = new Field[27];
            Fields = InitializeBoard(_fields);
        }

        public Field[] Fields
        {
            get
            {
                return this._fields;
            }
            private set { }
        }


        private Field[] InitializeBoard(Field[] field)
        {
            for (int i = 0; i < _fields.Length; i++)
            {
                field[i] = new Field();
            }
            field[1].WhiteCheckers = 2;
            field[6].RedCheckers = 5;
            field[8].RedCheckers = 3;
            field[12].WhiteCheckers = 5;
            field[13].RedCheckers = 5;
            field[17].WhiteCheckers = 3;
            field[19].WhiteCheckers = 5;
            field[24].RedCheckers = 2;
            return field;
        }


        public void MakeMove(int from, int to, Player player, GameBoard board)
        {
            if (player.Color.Equals("White"))
            {
                if (board.Fields[26].WhiteCheckers > 0)
                {
                    board.Fields[26].WhiteCheckers--;
                }
                else
                {
                    board.Fields[from].WhiteCheckers--;
                }
                board.Fields[to].WhiteCheckers++;
                if (board.Fields[to].RedCheckers == 1)
                {
                    board.Fields[to].RedCheckers--;
                    board.Fields[26].RedCheckers++;
                }
            }
            else
            {
                if (board.Fields[26].RedCheckers > 0)
                {
                    board.Fields[26].RedCheckers--;
                }
                else
                {
                    board.Fields[from].RedCheckers--;
                }
                board.Fields[to].RedCheckers++;
                if (board.Fields[to].WhiteCheckers == 1)
                {
                    board.Fields[to].WhiteCheckers--;
                    board.Fields[26].WhiteCheckers++;
                }
            }
        }
    }
}

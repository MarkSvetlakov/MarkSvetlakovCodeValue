using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Player
    {
        public string Name { get; private set; }
        public string Color { get; set; }
        public bool IsMyTurn { get; private set; }
        public int CheckersAtHome { get; set; }
        public int Moves { get; set; }
        public bool MoveFromHome { get; set; }
        private Random _random = new Random();


        public Player(string name, bool isHuman)
        {
            this.Name = name;
            this.MoveFromHome = false;
        }


        public void ChangeTurn()
        {
            this.IsMyTurn = !this.IsMyTurn;
        }


        public void ThrowDices(Dice dice)
        {
            dice.Throw();
        }


        public bool IsCanMove(IEnumerable<KeyValuePair<int, int>> listOfMoves, int[] fromTo)
        {
            if (!listOfMoves.Any())
            {
                Moves = 0;
            }

            foreach (var item in listOfMoves)
            {
                if (item.Key == fromTo[0] && item.Value == fromTo[1])
                {
                    return true;
                }
                if (item.Key == 26 && item.Value == fromTo[1])
                {
                    return true;
                }
            }
            return false;
        }
    }
}

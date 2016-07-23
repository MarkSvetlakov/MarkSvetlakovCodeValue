using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Dice
    {
        public int FirstCube { get;  private set; }
        public int SecondCube { get; private set; }
        public bool RolledDouble { get; private set; }
        private Random _random = new Random();

        public void Throw()
        {
            FirstCube = _random.Next(1, 7);
            SecondCube = _random.Next(1, 7);

            if (FirstCube == SecondCube)
            {
                RolledDouble = true;
            }
            else
            {
                RolledDouble = false;
            }
        }

        public void ResetFirstCube()
        {
            FirstCube = 0;
        }

        public void ResetSecondCube()
        {
            SecondCube = 0;
        }
    }
}

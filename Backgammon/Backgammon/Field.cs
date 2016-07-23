using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Field
    {
        public int RedCheckers { get; set; }
        public int WhiteCheckers { get; set; }

        public Field()
        {
            RedCheckers = 0;
            WhiteCheckers = 0;
        }
    }
}

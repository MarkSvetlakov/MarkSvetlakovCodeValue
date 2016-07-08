using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rationals
{
    class Program
    {
        static void Main(string[] args)
        {
            Rational r1 = new Rational(1, 2);
            Rational r2 = new Rational(1, 2);
            Rational r3;

            Console.WriteLine(r1 + r2);
            Console.WriteLine(r1 - r2);
            Console.WriteLine(r1 * r2);
            Console.WriteLine(r1 / r2);
            Console.WriteLine(r1.Equals(r2));

            Console.WriteLine((double)r1);
            Console.WriteLine(r1.ToDouble);

            r3 = (Rational)55;
        }
    }
}

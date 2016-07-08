using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLib
{
    public class Circle : Ellipse
    {
        public double Radius { get; private set; }
        public ConsoleColor Color { get; private set; }
        public Circle(int longAxle, int shortAxle , ConsoleColor color) : base(longAxle, shortAxle, color)
        {
            if (longAxle == shortAxle)
            {
                this.Radius = longAxle/2;
            }
            this.Color = color;
        }

        public override void Display()
        {
            Console.ForegroundColor = this.Color;
            Console.WriteLine("Circle redius is - "+ this.Radius);
            Console.ResetColor();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLib
{
    public abstract class Shape
    {
        public abstract double Area { get;}
        private ConsoleColor _color { get; set;}

        //Default constructor
        public Shape()
        {
            this._color = ConsoleColor.White;
        }

        //Constructor
        public Shape(ConsoleColor color)
        {
            this._color = color;
        }

        public virtual void Display()
        {
            Console.BackgroundColor  = this._color;
            Console.ResetColor();
        }

    }
}

using System;
using System.Diagnostics;

namespace Rationals
{
    public struct Rational
    {
        public int Numerator { get; private set; }
        public int Denominator { get; private set; }


        public Rational(int numerator, int denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
            _reduce();
        }

        public Rational(int numeratorValue)
        {
            this.Numerator = numeratorValue;
            this.Denominator = 1;
            _reduce();
        }


        public double ToDouble
        {
            get
            {
                return (double)this.Numerator / (double)this.Denominator;
            }
        }

        public static Rational operator +(Rational rational1, Rational rational2)
        {
            if (rational1._isUndefined || rational2._isUndefined)
            {
                return new Rational(0,0);
            }

            return new Rational((rational1.Numerator*rational2.Denominator) + (rational1.Denominator * rational2.Numerator),
                rational1.Denominator * rational2.Denominator);
        }


        public static Rational operator -(Rational rational1, Rational rational2)
        {
            if (rational1._isUndefined || rational2._isUndefined)
            {
                return new Rational(0, 0);
            }
            return new Rational((rational1.Numerator * rational2.Denominator) - (rational1.Denominator * rational2.Numerator),
                rational1.Denominator * rational2.Denominator);
        }


        public static Rational operator *(Rational rational1, Rational rational2)
        {
            if (rational1._isUndefined || rational2._isUndefined)
            {
                return new Rational(0, 0);
            }
            return new Rational(rational1.Numerator * rational2.Numerator,
                rational1.Denominator * rational2.Denominator);
        }


        public static Rational operator /(Rational rational1, Rational rational2)
        {
            if (rational1._isUndefined || rational2._isUndefined)
            {
                return new Rational(0, 0);
            }
            return new Rational(rational1.Numerator * rational2.Denominator,
                rational1.Denominator * rational2.Numerator);
        }


        public static explicit operator double(Rational value)
        {
            if (value.Denominator == 0)
            {
                return 0;
            }
            return (double)value.Numerator / value.Denominator;
        }


        public static implicit operator Rational(int value)
        {
            return new Rational(value);
        }


        private void _reduce()
        {
            int divisor = this._greatCommonDivisor(this.Numerator, this.Denominator);
            try
            {
                this.Numerator /= divisor;
                this.Denominator /= divisor;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }           
        }


        private int _greatCommonDivisor(int x, int y)
        {
            int temp;
            while (y != 0)
            {
                temp = x % y;
                x = y;
                y = temp;
            }
            return x;
        }

        public override string ToString()
        {
            if (this._isUndefined)
            {
                return "Undefined";
            }
            if (this._isInteger)
            {
                return this.Numerator.ToString();
            }
            return $"{this.Numerator}/{this.Denominator}";
        }


        private bool _equals(Rational other)
        {
            if (this._isUndefined)
            {
                return other._isUndefined;
            }
            return (this.Numerator == other.Numerator) && (this.Denominator == other.Denominator);
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }
            if (other is Rational)
            {
                return _equals((Rational)other);
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (this.Numerator == 0)
            {
                return 0;
            }
            return (this.Numerator.GetHashCode() * 397) ^ this.Denominator.GetHashCode();
        }

        private bool _isUndefined
        {
            get
            {
                return (this.Denominator == 0);
            }
        }

        private bool _isInteger
        {
            get
            {
                return (this.Denominator == 1);
            }
        }

    }
}

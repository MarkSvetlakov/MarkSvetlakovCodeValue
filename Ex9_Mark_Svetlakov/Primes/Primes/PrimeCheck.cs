using System;

namespace Primes
{
    public class PrimeCheck
    {
        public bool IsPrime(int number)
        {
            int boundary = (int)Math.Floor(Math.Sqrt(number));

            if (number == 1 || number == 0)
            {
                return false;
            }
            if (number == 2)
            {
                return true;
            }
            for (int i = 2; i <= boundary; ++i)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }


        public bool isValidNumbers(int firstNumber, int secondNumber)
        {
            if (firstNumber > secondNumber)
            {
                return false;
            }
            else if (firstNumber < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

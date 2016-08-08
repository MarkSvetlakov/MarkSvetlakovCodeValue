using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Primes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Numners returned before cancelled = {CalcPrimes(0, 30000000).Count}");
        }

        public static ConcurrentBag<int> CalcPrimes(int firstNum, int secondNum) //2.	Create a static method called CalcPrimes 
        {
            var resultList = new ConcurrentBag<int>();
            PrimeCheck primeCheck = new PrimeCheck();
            secondNum += 1;
            Random random = new Random();
            int randomResult;

            if (primeCheck.isValidNumbers(firstNum, secondNum))
            {
                Parallel.For(firstNum, secondNum, (numberToCheck, loopState) =>
                {
                    randomResult = random.Next(10000000);
                    if (randomResult == 0)
                    {
                        loopState.Stop();
                    }
                    if (primeCheck.IsPrime(numberToCheck))
                    {
                        resultList.Add(numberToCheck);
                    }
                });
            }
            return resultList;
            }
        }
}

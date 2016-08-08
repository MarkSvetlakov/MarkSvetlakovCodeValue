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
            var stopwatch = Stopwatch.StartNew();
            foreach (var item in CalcPrimes(0, 7, Environment.ProcessorCount))
            {
                Console.WriteLine(item);
            }
            stopwatch.Stop();
            Console.WriteLine($"With ProcessorCount maxDegree = {stopwatch.Elapsed}");

            stopwatch = Stopwatch.StartNew();
            foreach (var item in CalcPrimes(0, 7, 1))
            {
                Console.WriteLine(item);
            }
            stopwatch.Stop();
            Console.WriteLine($"With 1 maxDegree = {stopwatch.Elapsed}");


            stopwatch = Stopwatch.StartNew();
            foreach (var item in CalcPrimes(0, 7, 10))
            {
                Console.WriteLine(item);
            }
            stopwatch.Stop();
            Console.WriteLine($"With 10 maxDegree = {stopwatch.Elapsed}");
        }

        public static IEnumerable<int> CalcPrimes(int firstNum, int secondNum, int maxDegree) //2.	Create a static method called CalcPrimes 
        {
            var resultList = new ConcurrentBag<int>();
            ParallelOptions op = new ParallelOptions();
            op.MaxDegreeOfParallelism = maxDegree;
            PrimeCheck primeCheck = new PrimeCheck();
            secondNum += 1;


            if (primeCheck.isValidNumbers(firstNum, secondNum))
            {
                Parallel.For(firstNum, secondNum, op, numberToCheck =>
                {
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

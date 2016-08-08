using System;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;



namespace CustomAwaiter
{
    class Program
    {
        static void Main(string[] args)
        {
            Awaiter awaiter = new Awaiter(2000);
            awaiter.DoWork();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queues
{
    class Program
    {
        static void Main(string[] args)
        {
            LimitedQueue<int> queue = new LimitedQueue<int>(2);
            WorkWithQueue<int> workWithQueue = new WorkWithQueue<int>();

            workWithQueue.AddToQueue(queue, 5);
            workWithQueue.RemoveFromQueue(queue);
        }
    }
}

using System;
using System.Threading.Tasks;

namespace Queues
{
    class WorkWithQueue<T>
    {
        public void AddToQueue(LimitedQueue<T> queue, T value)
        {
            Random rnd = new Random();
            int count = rnd.Next(1, 5);
            while (count > 0)
            {
                Task.Factory.StartNew(() =>
                {
                    queue.Enqueue(value);
                }
                );
                count--;
            }
        }


        public void RemoveFromQueue(LimitedQueue<T> queue)
        {
            Random rnd = new Random();
            int count = rnd.Next(1, 5);
            while (count > 0)
            {
                Task.Factory.StartNew(() =>
                {
                    queue.Dequeue();
                }
                );
                count--;
            }
        }
    }
}

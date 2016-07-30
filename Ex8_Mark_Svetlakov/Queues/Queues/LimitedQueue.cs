using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queues
{
    class LimitedQueue<T> : IDisposable, IEnumerable<T>
    {
        private Queue<T> _queue = new Queue<T>();
        private Semaphore _semaphore;


        public LimitedQueue (int maxSize)
        {
            _semaphore = new Semaphore(0, maxSize);
        }


        public void Enqueue(T data)
        {
            if (data != null)
            {
                try
                {
                    _semaphore.Release();
                    lock (_queue)
                    {
                        _queue.Enqueue(data);
                    }
                }
                catch (SemaphoreFullException ex)
                {
                    Trace.TraceError(ex.Message);
                }
            }
        }


        public T Dequeue()
        {
            _semaphore.WaitOne();
            lock (_queue)
            {
                return _queue.Dequeue();
            }
        }


        void IDisposable.Dispose()
        {
            if (_semaphore != null)
            {
                _semaphore.Close();
                _semaphore = null;
            }
        }


        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

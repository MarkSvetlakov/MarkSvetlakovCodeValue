using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CustomAwaiter
{
    public class Awaiter : INotifyCompletion
    {
        private Action _continuation;
        public ManualResetEventSlim ResetEventSlim { get; }
        public int Delay { get; private set; }
        public bool IsCompleted { get; private set; }

        public Awaiter(int miliseconds)
        {
            ResetEventSlim = new ManualResetEventSlim();
            Delay = miliseconds;
        }


        public void OnCompleted(Action continuation)
        {
            _continuation += continuation;
        }


        private void GetResult()
        {
            if (IsCompleted)
            {
                return;
            }
            ResetEventSlim.Wait();
        }


        private void SetCompleted()
        {
            if (IsCompleted)
            {
                return;
            }
            IsCompleted = true;
            ResetEventSlim.Set();
            _continuation?.Invoke();
        }


        private void Wait(int miliseconds)
        {
            Task.Run(() =>
            {
                Thread.Sleep(miliseconds);
                SetCompleted();
            });
        }


        public void DoWork()
        {
            Console.WriteLine("Start Working...");

            if (Delay <= 0)
            {
                Delay = 0;
            }

            Wait(Delay);
            Action continuation = () => Console.WriteLine("Finished!");
            if (IsCompleted)
            {
                continuation();
            }
            else
            {
                GetResult();
                continuation();
            }
        }
    }
}

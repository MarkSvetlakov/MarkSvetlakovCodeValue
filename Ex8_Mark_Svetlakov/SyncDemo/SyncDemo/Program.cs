using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyncDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            LogWithMutex log = new LogWithMutex();

            log.Log();

            //var t = new Thread(o => log.Log());
            //t.Start();

            //var t2 = new Thread(o => log.Log());
            //t2.Start();
        }
    }
}

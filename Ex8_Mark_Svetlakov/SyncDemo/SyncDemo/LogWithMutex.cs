using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace SyncDemo
{

    public class LogWithMutex
    {
        public Mutex SyncFileMutex { get; private set; }


        public LogWithMutex()
        {
            SyncFileMutex = new Mutex();
        }


        public void Log()
        {
            string filePath = @"C:\Temp\";
            bool folderExists = Directory.Exists(filePath);

            if (!folderExists)
            {
                Directory.CreateDirectory(filePath);
            }

            SyncFileMutex.WaitOne();

            try
            {
                using (var w = File.AppendText(filePath + "data.txt"))
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        w.WriteLine(Process.GetCurrentProcess().Id);
                    }
                }
            }
            catch (IOException ex)
            {
                Trace.TraceError(ex.Message);
            }
            finally
            {
                SyncFileMutex.ReleaseMutex();
            }
        }
    }
}

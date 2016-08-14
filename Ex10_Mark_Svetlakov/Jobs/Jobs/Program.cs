using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobs
{
    class Program
    {
        static void Main(string[] args)
        {
            Job job = new Job();

            try
            {
                job.AddProcessToJob(Process.Start("notepad.exe"));
                job.AddProcessToJob(Process.Start("calc.exe"));
            }
            catch (Win32Exception ex)
            {
                Trace.TraceError(ex.Message);
                Console.WriteLine("Given process not exist");
            }
            catch (InvalidOperationException ex)
            {
                Trace.TraceError(ex.Message);
            }


            Console.WriteLine("Press Enter to close the job...");
            Console.ReadLine();
            job.Kill();

            for (int i = 0; i < 20; i++)
            {
                job = new Job(100);
            }

        }
    }
}

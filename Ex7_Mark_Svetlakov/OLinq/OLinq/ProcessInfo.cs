using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OLinq
{
    class ProcessInfo
    {
        private Process[] _processList;
        public int ProcessID { get; private set; }
        public string ProcessName { get; private set; }
        public DateTime ProcessStartTime { get; private set; }
        public StringBuilder strBuilder { get; private set; }
        public int SystemThreadsCount { get; private set; }

        public ProcessInfo()
        {
            _processList = Process.GetProcesses();
            strBuilder = new StringBuilder();
            NumOfSystemThreads();
        }

        public StringBuilder GetProcessInfo()
        {
            strBuilder.AppendLine("Processes info:");

            var priorities = _processList.GroupBy(x => x.BasePriority);

            foreach (var priority in priorities)
            {
                strBuilder.AppendLine($"Base priority: {priority.Key.ToString()}");
                foreach (var item in priority.Where(x => x.Threads.Count < 5).OrderByDescending(x => x.Id))
                {
                    try
                    {
                        ProcessID = item.Id;
                        ProcessName = item.ProcessName;
                        ProcessStartTime = item.StartTime;
                        strBuilder.AppendLine($"Process ID: {ProcessID}, Process Name: {ProcessName}, Start Time: {ProcessStartTime}");
                    }
                    catch (Win32Exception ex)
                    {
                        Trace.TraceError(ex.Message);
                        strBuilder.AppendLine($"Process ID: {ProcessID}, Process Name: {ProcessName}");
                    }
                }
            }
            return strBuilder;
        }

        private void NumOfSystemThreads()
        {
            foreach (var item in _processList)
            {
                SystemThreadsCount += item.Threads.Count;
            }
        }
    }
}

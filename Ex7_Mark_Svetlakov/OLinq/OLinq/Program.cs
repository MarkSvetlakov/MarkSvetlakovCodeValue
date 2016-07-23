using System;


namespace OLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessInfo pInfo = new ProcessInfo();
            Console.WriteLine(pInfo.GetProcessInfo());
            Console.WriteLine(pInfo.SystemThreadsCount);
            MscorlibInfo MsInfo = new MscorlibInfo();
            Console.WriteLine(MsInfo.GetAssemblyInfo());
        }
    }
}

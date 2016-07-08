using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager manager = new FileManager();
            List<string> dataList = manager.ReadData("Names.txt");

            if (dataList != null && dataList.Any())
            {
                foreach (string item in dataList)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("There is no data to display");
            }
        }
    }
}

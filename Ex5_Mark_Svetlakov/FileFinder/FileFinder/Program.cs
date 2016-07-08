using System;
using System.Collections;

namespace FileFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSearcher fs = new FileSearcher(args);
            Console.WriteLine(fs.SearchResult());
        }
    }
}
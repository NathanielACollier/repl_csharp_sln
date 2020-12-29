using System;
using System.Linq;

namespace DEC_01
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootPath = commonUtilitiesLib.Directory.getRootDirectory();

            Console.WriteLine($"Root Path: [{rootPath}]");
        }


    }
}

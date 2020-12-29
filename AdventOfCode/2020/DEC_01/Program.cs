using System;
using System.Linq;

namespace DEC_01
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootPath = getRootDirectory();

            Console.WriteLine($"Root Path: [{rootPath}]");
        }

        static string getRootDirectory()
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string dirPath = System.IO.Path.GetDirectoryName(exePath);

            // find parent that is key
            string key = "repl_csharp_sln";
            var pieces = dirPath.Split('/', '\\').Select(p => p.ToLower());

            var rootIndex = pieces.ToList().FindIndex(p => string.Equals(p, key, StringComparison.OrdinalIgnoreCase));

            var rootPathList = pieces.Take(rootIndex + 1);

            string rootPath = string.Join(System.IO.Path.DirectorySeparatorChar, rootPathList);

            return rootPath;
        }
    }
}

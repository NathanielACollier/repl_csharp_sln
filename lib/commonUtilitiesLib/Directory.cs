using System;
using System.Linq;

namespace commonUtilitiesLib
{
    public static class Directory
    {


        public static string getRootDirectory()
        {
            string dirPath = AppContext.BaseDirectory;

            // find parent that is key
            string key = "repl_csharp_sln";
            var pieces = dirPath.Split('/', '\\').Select(p => p.ToLower());

            var rootIndex = pieces.ToList().FindIndex(p => string.Equals(p, key, StringComparison.OrdinalIgnoreCase));

            var rootPathList = pieces.Take(rootIndex + 1);

            string rootPath = string.Join(System.IO.Path.DirectorySeparatorChar, rootPathList);

            // make sure it has ending path seperator
            if (rootPath.TrimEnd().Last() != System.IO.Path.DirectorySeparatorChar)
            {
                rootPath = rootPath.TrimEnd() + System.IO.Path.DirectorySeparatorChar;
            }

            return rootPath;
        }



    }
}

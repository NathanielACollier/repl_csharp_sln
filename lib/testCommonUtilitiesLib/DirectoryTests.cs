using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testCommonUtilitiesLib
{
    [TestClass]
    public class DirectoryTests
    {
        [TestMethod]
        public void RootDirContainsRootFolderName()
        {
            string rootDirPath = commonUtilitiesLib.Directory.getRootDirectory();

            Assert.IsTrue(rootDirPath.IndexOf("repl_csharp_sln", System.StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}

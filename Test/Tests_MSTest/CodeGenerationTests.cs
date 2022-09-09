

using Microsoft.VisualStudio.TestTools.UnitTesting;

using log = Tests_MSTest.lib.log;

namespace Tests_MSTest;

[TestClass]
public class CodeGenerationTests
{


    [TestMethod]
    public void FileToStaticByteArray()
    {
        var data = System.IO.File.ReadAllBytes(@"C:\Users\nathaniel\Desktop\Untitled.bmp");

        string codeGen = $@"
            byte[] imgData = new[]{{
                {string.Join(',', data.Select(b => b.ToString()))}
            }}
        ".Trim();

        log.raw(codeGen);
    }



}

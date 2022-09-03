using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testCommonUtilitiesLib
{
    [TestClass]
    public class StringUtilTests
    {


        [TestMethod]
        public void ByteSizeSimpleTests()
        {
            string words = commonUtilitiesLib.StringUtil.byteSizeToString(9438342842348234);
            
            Assert.IsTrue(!string.IsNullOrWhiteSpace(words));
        }
    }
}
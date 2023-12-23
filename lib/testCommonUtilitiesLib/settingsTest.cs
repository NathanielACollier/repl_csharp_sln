using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testCommonUtilitiesLib;

[TestClass]
public class settingsTest
{

    [TestMethod]
    public void GetSetting(){
        string val = commonUtilitiesLib.settings.Get("test");

        Assert.IsTrue(!string.IsNullOrWhiteSpace(val), "Test setting was empty");
        Assert.IsTrue(val.Length > 3, "Test setting wasn't as long as it should have been");
    }
}

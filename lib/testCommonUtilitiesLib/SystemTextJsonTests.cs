using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testCommonUtilitiesLib;

[TestClass]
public class SystemTextJsonTests
{


    [TestMethod]
    public void StringOnlyTest()
    {
        string result = commonUtilitiesLib.SystemTextJson.DeserializeToDictionaryList(@"
            ""Hello World""
        ") as string;
        
        Assert.IsTrue(!string.IsNullOrWhiteSpace(result));
    }

    [TestMethod]
    public void ListOneLevel()
    {
        dynamic result = commonUtilitiesLib.SystemTextJson.DeserializeToDictionaryList(@"
            [
                {""A"": 1},
                {""A"": 2},
                {""A"": 3}
            ]
        ");

        Assert.IsTrue(result[0]["A"] == 1);
        Assert.IsTrue(result[1]["A"] == 2);
    }
}

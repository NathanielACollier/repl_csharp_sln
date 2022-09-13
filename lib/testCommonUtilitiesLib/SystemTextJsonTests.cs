using System.Collections.Generic;
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
        var result = commonUtilitiesLib.SystemTextJson.DeserializeToDictionaryList(@"
            [
                {""A"": ""1""},
                {""A"": ""2""},
                {""A"": ""3""}
            ]
        ") as List<Dictionary<string, object>>;
        
        Assert.IsTrue(result[0]["A"] as int? == 1);
        Assert.IsTrue(result[1]["A"] as int? == 2);
    }
}

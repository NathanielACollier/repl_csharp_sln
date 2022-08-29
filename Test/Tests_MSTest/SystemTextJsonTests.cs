namespace Tests_MSTest;

[TestClass]
public class SystemTextJsonTests
{
    [TestMethod]
    public void ReadTextBasic()
    {
        string jsonText = @"
            {
                ""a"": ""Apple"",
                ""b"": ""Orange""
            }
        ";
        
        dynamic result = System.Text.Json.Nodes.JsonNode.Parse(jsonText);
        
        Assert.IsTrue(result != null);
        
        Assert.IsTrue(string.Equals(result.a, "Apple",StringComparison.OrdinalIgnoreCase));
    }


    [TestMethod]
    public void ReadAsDictionary()
    {
        string jsonText = @"
            {
                ""a"": ""Apple"",
                ""b"": ""Orange""
            }
        ";

        var dict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(jsonText);
        
        Assert.IsTrue(string.Equals( dict["a"], "Apple", StringComparison.OrdinalIgnoreCase));
    }
}
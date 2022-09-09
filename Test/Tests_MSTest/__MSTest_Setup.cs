using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests_MSTest;

[TestClass]
public class __MSTest_Setup
{

    [AssemblyInitialize()]
    public static void MyTestInitialize(TestContext testContext)
    {
        System.Diagnostics.Debug.WriteLine("Test Starting");
        
        // This runs one time on starting the tests
        // Add logging, or setting up variables here
    }

    [AssemblyCleanup]
    public static void TearDown()
    {
            
            
    }
}
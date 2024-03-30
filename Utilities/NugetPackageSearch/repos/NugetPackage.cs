namespace NugetPackageSearch.repos;

public class NugetPackage
{
    private NugetPackage()
    {
        
    }

    public static async Task<NugetPackage> Create()
    {
        var nuget = new NugetPackage();
        await nuget.Init();
        
        return nuget;
    }

    private async Task Init()
    {
        var settings = new NuGet.Configuration.Settings();
        var packageProvier = new NuGet.Configuration.PackageSourceProvider(settings);
        
        
    }
    
    
    
    
}
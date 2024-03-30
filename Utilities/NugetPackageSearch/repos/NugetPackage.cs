using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Protocol.Core.Types;

namespace NugetPackageSearch.repos;

public class NugetPackage
{
    private models.Nuget.CommandLineSourceRepositoryProvider provider;
    
    private NugetPackage()
    {
        
    }

    public static async Task<NugetPackage> Create()
    {
        var nuget = new NugetPackage();
        await nuget.Init();
        
        return nuget;
    }

    private string getNugetRootPath()
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        string NugetPath = System.IO.Path.Combine(appDataPath, "Nuget");

        // create or get existing directory
        var nugetDir = System.IO.Directory.CreateDirectory(NugetPath);

        return nugetDir.FullName;
    }
    
    private Task Init()
    {
        return Task.Run(() =>
        {
            var settings = new NuGet.Configuration.Settings(root: getNugetRootPath());
            var packageProvier = new NuGet.Configuration.PackageSourceProvider(settings);
            
            provider = new models.Nuget.CommandLineSourceRepositoryProvider(packageProvier);
        });

    }
    
    public async Task<IReadOnlyList<IPackageSearchMetadata>> GetPackagesAsync(string searchTerm, bool includePrerelease, bool exactMatch)
    {
        var filter = new SearchFilter(includePrerelease);
        var packages = new List<IPackageSearchMetadata>();

        foreach (var sourceRepository in provider.GetRepositories())
        {
            var searchResource = await sourceRepository.GetResourceAsync<PackageSearchResource>();
            
            /*
             2023-03-30 - Looking through this Search function: https://github.com/open-rpa/openrpa/blob/9448896d91429cfc7b0733cf7bc3de559116b8cc/OpenRPA/NuGet/NuGetPackageManager.cs#L69
             */

            var result = (
                await searchResource.SearchAsync(searchTerm, filter, 0, 20, NullLogger.Instance,
                    CancellationToken.None)
            ).ToList();

            if (exactMatch)
            {
                var match = result.FirstOrDefault(c => string.Equals(c.Identity.Id, searchTerm,
                    StringComparison.OrdinalIgnoreCase));
                result = match != null ? [match] : null;
            }

            if (result.Count > 0)
            {
                packages.AddRange(result);
            }
        }

        return packages;
    }
    
    
    
}
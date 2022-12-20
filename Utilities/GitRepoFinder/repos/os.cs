using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GitRepoFinder.repos;

public class os
{
    
    /*
     Original code from here: https://github.com/dotnet/runtime/issues/17938
     */
    public static void OpenBrowser(string url)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Process.Start("xdg-open", url);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", url);
        }
        else
        {
            throw new Exception($"Unknown platform: [{RuntimeInformation.OSDescription}]");
        }
    }
    
    
}
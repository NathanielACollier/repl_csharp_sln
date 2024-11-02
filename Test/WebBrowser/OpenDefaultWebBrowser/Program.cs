using System.Diagnostics;
using System.Runtime.InteropServices;


OpenBrowser("https://www.google.com");


/*
 Maintain notes on what you encounter with this here
 + Original code is from: https://github.com/dotnet/runtime/issues/17938
 */

System.Diagnostics.Process OpenBrowser(string url)
{
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        url = url.Replace("&", "^&");
        return Process.Start(new ProcessStartInfo("cmd", $"/c start {url}")
        {
            CreateNoWindow = true
        });
    }
    
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
        return Process.Start("xdg-open", url);
    }
    
    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    {
        return Process.Start("open", url);
    }

    throw new NotImplementedException(
        $"Your browser is not implemented.   Your OS: [{RuntimeInformation.OSDescription}]");
}
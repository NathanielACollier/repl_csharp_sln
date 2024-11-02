using System.Diagnostics;
using System.Runtime.InteropServices;


OpenBrowser("https://www.google.com");


System.Diagnostics.Process OpenBrowser(string url)
{
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        return Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
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
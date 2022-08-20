using PuppeteerSharp;

var browser = await getBrowser();

Console.WriteLine("App finished");


async Task<Browser> getBrowser()
{
    var browser = await Puppeteer.LaunchAsync(new LaunchOptions
    {
        Headless = false,
        ExecutablePath = findChrome(),
        Args = new[] { "--app=http://localhost/null" }
    });

    return browser;
}


string findChrome()
{
    var chromeProcs = System.Diagnostics.Process.GetProcessesByName("chrome");

    if (!chromeProcs.Any())
    {
        throw new Exception("Chrome must be running for this code to find it");
    }

    return chromeProcs.First().Modules[0].FileName;
}

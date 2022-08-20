using PuppeteerSharp;

var browser = await getBrowser();

Console.WriteLine("App finished");



async Task<Browser> getBrowser()
{
    int browserRevicision = BrowserFetcher.DefaultRevision;

    string homeFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    string nacPuppeteerPath = System.IO.Path.Combine(homeFolderPath, ".nacPuppeteer");

    string currentBrowserVersionFolderPath = System.IO.Path.Combine(nacPuppeteerPath, $"chrome_{browserRevicision}");

    if(!System.IO.Directory.Exists(currentBrowserVersionFolderPath))
    {
        // download it

    }

    var info = await new BrowserFetcher(new BrowserFetcherOptions
    {
        Path = currentBrowserVersionFolderPath
    }).DownloadAsync(BrowserFetcher.DefaultRevision);

    // now the browser is either in the path as just downloaded, or was already there

    var browser = await Puppeteer.LaunchAsync(new LaunchOptions
    {
        Headless = true
    ,
        ExecutablePath = info.ExecutablePath
        //Args = new string[]{ "--auth-server-whitelist=*.ecark.org,*.aecc.com,*.arkansaselectric.com" }
    });

    return browser;
}
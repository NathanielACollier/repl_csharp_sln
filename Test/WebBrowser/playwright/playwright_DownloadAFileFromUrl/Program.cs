
using Microsoft.Playwright;

await useBrowser(async (browser,page)=>{
    await downloadFileTest(page);
});

async Task downloadFileTest(IPage page)
{
    /*
    Goal is to download the treasury rate PDF on this page
    https://www.rd.usda.gov/page/rural-utilities-loan-interest-rates
    The PDF Url is:
    https://www.rd.usda.gov/media/file/download/ffb-daily-rates.pdf
    */

    var fileData = await DownloadFile(page, "https://www.rd.usda.gov/media/file/download/ffb-daily-rates.pdf");

    commonUtilitiesLib.File.WriteAllBytes("file.pdf", fileData);
}


async Task<byte[]> DownloadFile(IPage page, string urlToDownload){
    string htmlText = $@"
        <html>
            <body>
                <a href='{urlToDownload}'>Download my file</a>
            </body>
        </html>
    ";
    commonUtilitiesLib.File.WriteAllText("test.html", htmlText);

    await page.SetContentAsync(html: htmlText);

    var downloadEvent = page.WaitForDownloadAsync();

    /*
    Look at this link where they use an Alt click to try and force the download
    https://github.com/microsoft/playwright-python/issues/675
    */
    await Task.WhenAll(
        downloadEvent,
        page.ClickAsync("a", options: new PageClickOptions{
             Modifiers = new[]{ KeyboardModifier.Alt }
        })
    );

    var stream = await downloadEvent.Result.CreateReadStreamAsync();
    using (var ms = new System.IO.MemoryStream()) {
        stream.Seek(0, SeekOrigin.Begin);
        stream.CopyTo(ms);
        return ms.ToArray();
    }
}

async Task useBrowser(Func<Microsoft.Playwright.IBrowser, Microsoft.Playwright.IPage, Task> onNewPageAvailable)
{
    using (var playwright = await Microsoft.Playwright.Playwright.CreateAsync())
    {
        await using (var browser = await playwright.Chromium.LaunchAsync(new()
                        {
                            Headless = true,
                            // Can be "msedge", "chrome-beta", "msedge-beta", "msedge-dev", etc.
                            // see: https://playwright.dev/dotnet/docs/browsers#chromium
                            Channel = "chrome"
                        }))
        {
            var context = await browser.NewContextAsync(options: new BrowserNewContextOptions{
                AcceptDownloads = true
            });
            var page = await context.NewPageAsync();
            try
            {
                await onNewPageAvailable(browser, page);
            }
            finally
            {
                await page.CloseAsync();
                await browser.CloseAsync();
            }
        }
    }
}
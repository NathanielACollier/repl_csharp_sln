
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
}


async Task<byte[]> DownloadFile(IPage page, string urlToDownload){
    string htmlText = $@"
        <html>
            <body>
                <a href='{urlToDownload}'>Download my file</a>
            </body>
        </html>
    ";

    await page.SetContentAsync(html: htmlText);

    // use this method: https://trycatchdebug.net/news/1273806/playwright-pdf-download
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
            var page = await browser.NewPageAsync();
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
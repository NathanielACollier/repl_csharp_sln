
await useBrowser(async (browser,page) => {
    await page.GotoAsync("https://news.ycombinator.com");
    
    Console.WriteLine("Press any key...");
    Console.Read();
});


async Task useBrowser(Func<Microsoft.Playwright.IBrowser, Microsoft.Playwright.IPage, Task> onNewPageAvailable)
{
    using (var playwright = await Microsoft.Playwright.Playwright.CreateAsync())
    {
        await using (var browser = await playwright.Chromium.LaunchAsync(new()
                        {
                            Headless = false,
                            // Can be "msedge", "chrome-beta", "msedge-beta", "msedge-dev", etc.
                            // see: https://playwright.dev/dotnet/docs/browsers#chromium
                            Channel = "chrome"
                        }))
        {
            var context = await browser.NewContextAsync(options: new Microsoft.Playwright.BrowserNewContextOptions
            {
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
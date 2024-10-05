
using System.IO.Compression;
using nac.http.model;

var log = new nac.Logging.Logger();
var http = new nac.http.HttpClient( args: new HttpClientConfigurationOptions
{
    timeout = new TimeSpan(0,0,60)
});

//var harManager = new nac.http.logging.har.lib.HARLogManager("http.har");

nac.Logging.Appenders.ColoredConsole.Setup();

log.Info("Application started");


http.AddDefaultRequestHeader("User-Agent",
    "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36");
http.AddDefaultRequestHeader("Accept-Language",
    "en-US,en;q=0.9");
http.AddDefaultRequestHeader("Accept-Encoding",
    "gzip, deflate, br, zstd");
http.AddDefaultRequestHeader("Accept",
    "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
http.AddDefaultRequestHeader("Referer",
    "https://www.google.com");
http.AddDefaultRequestHeader("Cache-Control",
    "no-cache");
http.AddDefaultRequestHeader("Pragma",
    "no-cache");
http.AddDefaultRequestHeader("Priority",
    "u=0, i");
http.AddDefaultRequestHeader("Sec-Ch-Ua",
    "\"Google Chrome\";v=\"129\", \"Not=A?Brand\";v=\"8\", \"Chromium\";v=\"129\"");
http.AddDefaultRequestHeader("Sec-Ch-Ua-Mobile",
    "70");
http.AddDefaultRequestHeader("Sec-Ch-Ua-Platform",
    "\"Linux\"");
http.AddDefaultRequestHeader("Sec-Fetch-Dest",
    "document");
http.AddDefaultRequestHeader("Sec-Fetch-Mode",
    "navigate");
http.AddDefaultRequestHeader("Sec-Fetch-Site",
    "none");
http.AddDefaultRequestHeader("Sec-Fetch-User", 
    "71");
http.AddDefaultRequestHeader("Upgrade-Insecure-Requests",
    "1");

try
{
    byte[] rusHtmlData =
        await http.GetJSONAsync<byte[]>("https://www.rd.usda.gov/page/rural-utilities-loan-interest-rates");

    string htmlText = GetHtmlFromGzip(rusHtmlData);
    
    log.Info($"\n---\nHTML\n---\n{htmlText}");
}
catch (Exception ex)
{
    log.Error($"Page download failure:\n{ex}");
}


log.Info("Application stopped");



string GetHtmlFromGzip(byte[] data)
{
    using (var httpDataGzipped = new System.IO.MemoryStream(data))
    {
        using (var gzipStream = new System.IO.Compression.GZipStream(httpDataGzipped, CompressionMode.Decompress))
        {
            using (var uncompressedOutStream = new System.IO.MemoryStream())
            {
                gzipStream.CopyTo(uncompressedOutStream);

                byte[] uncompressedData = uncompressedOutStream.ToArray();

                string utf8Text = System.Text.Encoding.UTF8.GetString(uncompressedData);
                return utf8Text;
            }
        }
    }
}

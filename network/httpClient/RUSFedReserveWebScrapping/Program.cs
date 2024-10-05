
var log = new nac.Logging.Logger();
var http = new nac.http.HttpClient();
var harManager = new nac.http.logging.har.lib.HARLogManager("http.har");

nac.Logging.Appenders.ColoredConsole.Setup();

log.Info("Application started");


http.AddDefaultRequestHeader("User-Agent",
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
http.AddDefaultRequestHeader("Accept-Language",
    "en-US,en;q=0.9,es;q=0.8");
http.AddDefaultRequestHeader("Accept-Encoding",
    "gzip, deflate, br");
http.AddDefaultRequestHeader("Accept",
    "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
http.AddDefaultRequestHeader("Referer",
    "https://www.google.com");

string rusHtml = await http.GetJSONAsync<string>("https://www.rd.usda.gov/page/rural-utilities-loan-interest-rates");

log.Info($"\n---\nHTML\n---\n{rusHtml}");

log.Info("Application stopped");
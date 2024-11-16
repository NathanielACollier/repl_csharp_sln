
var log = new nac.Logging.Logger();
nac.Logging.Appenders.ColoredConsole.Setup();

try
{
    log.Info("Application started");
    await run();
}
catch (Exception ex)
{
    log.Error($"General exception.  {ex}");
}
log.Info("Application finished");


async Task run()
{

    var webServer = startWebServerToHandleOauthRedirect();
}

nac.WebServer.WebServerManager startWebServerToHandleOauthRedirect(){
    var webServer = new nac.WebServer.WebServerManager();

    webServer.OnNewRequest += (_s, args) =>
    {

    };

    return webServer;
}


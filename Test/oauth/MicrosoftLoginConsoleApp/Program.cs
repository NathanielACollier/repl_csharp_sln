
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;
using nac.WebServer.lib;

var log = new nac.Logging.Logger();
nac.Logging.Appenders.ColoredConsole.Setup();
TaskCompletionSource<OauthCodeReceivedResult> waitOnCodeReceivedByLocalWebServer = new();

var oauthSettings = new OpenAuthenticationSettings
{
    ClientId = commonUtilitiesLib.settings.Get("TestAuthAzureAppID"),
    ClientSecret = commonUtilitiesLib.settings.Get("TestAuthAzureClientSecret"),
    ForceSelectAccount = true,
    Tenant = "common",
    RedirectUrl = "", // Note: This will get modified by the local url manager
    Scope = "https://graph.microsoft.com/.default",
    State = ""
};


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
    /*
     # Steps
     + Build Login URL
     + Start a web server to listen to a redirect URL
     + Open web browser to the login URL that has a redirect URL of our local URL
     */
    
    log.Info("Starting local web server");
    var webServer = startWebServerToHandleOauthRedirect();
    oauthSettings.RedirectUrl = webServer.Url; // now we have a local URL we can set the Redirect Url
    log.Info($"Local web server running: {webServer.Url}");
    
    // now we are ready to get the login url
    string loginUrl = GetMicrosoftLoginUrl();
    log.Info($"Got Microsoft Login Url: {loginUrl}");
    
    log.Info("Opening default browser so user can login");
    // now we need to show that URL to the user in a web browser
    nac.WebServer.lib.BrowserUtility.OpenBrowser(loginUrl);

    log.Info("Waiting on Code from Microsoft");
    var oauthCodeResult = await waitOnCodeReceivedByLocalWebServer.Task;
    log.Info($"Got code back from Microsoft.   Code size: {oauthCodeResult.Code.Length}");
    
    log.Info("Retrieving token from Microsoft");
    string token = await GetMicrosoftBearerTokenFromOauthCode(oauthCodeResult.Code);
    log.Info($"Got back token from Microsoft.  Token size: {token.Length}");
    
    
}

nac.WebServer.WebServerManager startWebServerToHandleOauthRedirect(){
    var webServer = new nac.WebServer.WebServerManager();

    webServer.OnNewRequest += (_s, args) =>
    {
        if (args.Request.Url.LocalPath == "/")
        {
            string code = args.Request.QueryString["code"];

            waitOnCodeReceivedByLocalWebServer.SetResult(new OauthCodeReceivedResult
            {
                Code = code,
                RedirectUrl = webServer.Url
            });
            
            args.Response.WriteHtmlResponse(@"
                <div style='color:green;font-weight:bold;'>You can close the browser now.   Microsoft OAuth Code has been retrieved.</div>
            ");
        }

    };
    
    webServer.Start();

    return webServer;
}




string GetMicrosoftLoginUrl()
{

    var loginUrl = new nac.utilities.Url("https://login.microsoftonline.com/");

    loginUrl.Path = $"{oauthSettings.Tenant}/oauth2/v2.0/authorize";

    // add the params
    loginUrl.AddQueryParametersFromDictionary(new Dictionary<string, string>{
        {"client_id", oauthSettings.ClientId},
        {"response_type", "code"},
        {"redirect_uri", oauthSettings.RedirectUrl},
        {"scope", oauthSettings.Scope},
        {"response_mode", "query"},
        {"state", oauthSettings.State}
    });

    if (oauthSettings.ForceSelectAccount)
    {
       loginUrl.AddQuery("prompt", "select_account");
    }

    return loginUrl.ToString();
}


async Task<string> GetMicrosoftBearerTokenFromOauthCode(string code)
{
    var base_url = "https://login.microsoftonline.com/";
    
    // add the params
    var values = new Dictionary<string, string>{
        {"grant_type","authorization_code" },
        {"client_id", oauthSettings.ClientId},
        {"client_secret",oauthSettings.ClientSecret },    
        {"scope", oauthSettings.Scope},
        {"code", code},
        {"redirect_uri", oauthSettings.RedirectUrl},
    };
    var httpClient = new nac.http.HttpClient(baseUrl: base_url,
        useWindowsAuth: false);

    var response = await httpClient.PostFormUrlEncodeAsync<System.Text.Json.Nodes.JsonNode>($"{oauthSettings.Tenant}/oauth2/v2.0/token",
        values: values);

    string token = response["access_token"].Deserialize<string>();

    return token;
}




class OpenAuthenticationSettings
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public bool ForceSelectAccount { get; set; }
    public string Tenant { get; set; }
    public string RedirectUrl { get; set; }
    public string Scope { get; set; }
    public string State { get; set; }
}


class OauthCodeReceivedResult
{
    public string Code { get; set; }
    public string RedirectUrl { get; set; }
}
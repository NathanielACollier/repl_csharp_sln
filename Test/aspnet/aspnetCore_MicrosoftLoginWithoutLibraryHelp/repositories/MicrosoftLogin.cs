using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;

namespace aspnetCore_MicrosoftLoginWithoutLibraryHelp.repositories;


/*
Follow the documentation here:
https://learn.microsoft.com/en-us/entra/identity-platform/v2-oauth2-client-creds-grant-flow
*/

public static class MicrosoftLogin
{
    private static nac.Logging.Logger log = new();
    private static string graphScope = "https://graph.microsoft.com/.default";


    public static void RedirectIfNotLoggedIn(Microsoft.AspNetCore.Http.HttpContext httpContext)
    {
        string urlAttempted = httpContext.Request.GetEncodedUrl();
        log.Info($"Attempting to go to URL: {urlAttempted}");

        var loginCodeUrl = new nac.utilities.Url(urlAttempted);
        loginCodeUrl.Path = "api/general/msLogin";
        loginCodeUrl.ClearQuery();
        log.Info($"Redirect to Microsoft Login, and have them send code to: {loginCodeUrl}");

        string loginUrl = FormMicrosoftLoginUrl(redirectUrl: loginCodeUrl.ToString(),
        state: new lib.MSLoginSaveState{
            OriginalUrl = urlAttempted
        });

        log.Info($"Microsoft Login URL is: {loginUrl}");

        throw new lib.HttpRedirectFiltering.HttpRedirectException(url: loginUrl);
    }

    private static (string appID, string clientSecret) readEntraSettings(){
        return (appID: commonUtilitiesLib.settings.Get("TestAuthAzureAppID"),
        clientSecret: commonUtilitiesLib.settings.Get("TestAuthAzureClientSecret")
        );
    }

    private static string FormMicrosoftLoginUrl(string redirectUrl, lib.MSLoginSaveState state){
        string tenant = "common"; // your appID has to be multi tentant to use the common tenant
        var entraSettings = readEntraSettings();

        var loginUrl = new nac.utilities.Url("https://login.microsoftonline.com/");
        
        loginUrl.Path = $"{tenant}/oauth2/v2.0/authorize";

        // add the params
        loginUrl.AddQueryParametersFromDictionary(new Dictionary<string,string>{
            {"client_id", entraSettings.appID},
            {"response_type", "code"},
            {"redirect_uri", redirectUrl},
            {"response_mode", "query"},
            {"scope", graphScope},
            {"state", CreateMSLoginState(state)}
        });

        return loginUrl.ToString();
    }


    private static string CreateMSLoginState(lib.MSLoginSaveState state){
        string jsonText = System.Text.Json.JsonSerializer.Serialize(state);
        var utf8Bytes = System.Text.Encoding.UTF8.GetBytes(jsonText);
        return System.Convert.ToBase64String(utf8Bytes);
    }


    public static lib.MSLoginSaveState ReadState(string stateBase64Encoded){
        byte[] data = System.Convert.FromBase64String(stateBase64Encoded);
        string jsonText = System.Text.Encoding.UTF8.GetString(data);
        var state = System.Text.Json.JsonSerializer.Deserialize<lib.MSLoginSaveState>(json: jsonText);
        return state;
    }

    public static async Task<string> GetTokenFromCode(string code)
    {
        
    }
}

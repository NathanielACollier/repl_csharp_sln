using System;
using System.Collections.Generic;

namespace aspnetCore_MicrosoftLoginWithoutLibraryHelp;

public static class MicrosoftLogin
{
    private static string graphRootUrl = "https://graph.microsoft.com";

    private static (string appID, string clientSecret) readEntraSettings(){
        return (appID: commonUtilitiesLib.settings.Get("TestAuthAzureAppID"),
        clientSecret: commonUtilitiesLib.settings.Get("TestAuthAzureClientSecret")
        );
    }

    private static string FormMicrosoftLoginUrl(string redirectUrl){
        string tenant = "common"; // your appID has to be multi tentant to use the common tenant
        var entraSettings = readEntraSettings();

        var loginUrl = new nac.utilities.Url("https://login.microsoftonline.com/");
        
        loginUrl.Path = $"{tenant}/oauth2/authorize";

        // add the params
        loginUrl.AddQueryParametersFromDictionary(new Dictionary<string,string>{
            {"client_id", entraSettings.appID},
            {"response_type", "code"},
            {"redirect_uri", redirectUrl},
            {"response_mode", "query"},
            {"resource", graphRootUrl},
            {"state", "12345"}
        });

        return loginUrl.ToString();
    }

}

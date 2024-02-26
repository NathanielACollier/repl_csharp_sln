﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Extensions;

namespace aspnetCore_MicrosoftLoginWithoutLibraryHelp.repositories;

public static class MicrosoftLogin
{
    private static nac.Logging.Logger log = new();
    private static string graphRootUrl = "https://graph.microsoft.com";


    public static void RedirectIfNotLoggedIn(Microsoft.AspNetCore.Http.HttpContext httpContext)
    {
        string urlAttempted = httpContext.Request.GetEncodedUrl();
        log.Info($"Attempting to go to URL: {urlAttempted}");

        var loginCodeUrl = new nac.utilities.Url(urlAttempted);
        loginCodeUrl.Path = "api/general/loginWithOffice365Code";
        loginCodeUrl.ClearQuery();
        log.Info($"Redirect to Microsoft Login, and have them send code to: {loginCodeUrl}");

        string loginUrl = FormMicrosoftLoginUrl(redirectUrl: loginCodeUrl.ToString());
        log.Info($"Microsoft Login URL is: {loginUrl}");
        
        throw new lib.HttpRedirectFiltering.HttpRedirectException(url: loginUrl);
    }

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

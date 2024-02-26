using System;

namespace aspnetCore_MicrosoftLoginWithoutLibraryHelp.lib.HttpRedirectFiltering;

public class HttpRedirectException : Exception
{
    public string Url {get; set;}

    public HttpRedirectException(string url): base($"Redirect to {url}"){
        this.Url = url;
    }
}

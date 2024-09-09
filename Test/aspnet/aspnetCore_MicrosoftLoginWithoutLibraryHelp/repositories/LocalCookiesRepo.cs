using System;
using Microsoft.AspNetCore.Http;

namespace aspnetCore_MicrosoftLoginWithoutLibraryHelp.repositories;

public class LocalCookiesRepo
{

    private HttpContext httpContext;
    
    public LocalCookiesRepo(HttpContext httpContext)
    {
        this.httpContext = httpContext;
    }

    public bool? forceAccountSelect
    {
        get
        {
            if (httpContext.Request.Cookies.TryGetValue("forceAccountSelect", out string forceAccountSelectStr))
            {
                return Convert.ToBoolean(forceAccountSelectStr);
            }

            return null;
        }
    }


    private const string cookieName_office365token = "office365_token";
    public string office365token
    {
        get
        {
            if (httpContext.Request.Cookies.TryGetValue(cookieName_office365token, out string office365_token))
            {
                return office365_token;
            }

            return null;
        }
        set
        {
            httpContext.Response.Cookies.Append(cookieName_office365token, value);
        }
    }
    
    
}
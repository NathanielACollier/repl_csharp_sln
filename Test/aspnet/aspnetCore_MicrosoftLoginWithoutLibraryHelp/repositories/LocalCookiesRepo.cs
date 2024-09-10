using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace aspnetCore_MicrosoftLoginWithoutLibraryHelp.repositories;

public class LocalCookiesRepo
{

    private HttpContext httpContext;

    /*
     Need a local place to save any cookies we set on the response, because it's hard to read them back if we need to access them later
    */
    private Dictionary<string,string> localResponseCookiesTemp = new();
    
    public LocalCookiesRepo(HttpContext httpContext)
    {
        this.httpContext = httpContext;
    }

    public bool? forceAccountSelect
    {
        get
        {
            string cookieValue = this.getCookie("forceAccountSelect");

            if(cookieValue != null){
                return Convert.ToBoolean(cookieValue);
            }

            return null;
        }
    }


    private const string cookieName_office365token = "office365_token";
    public string office365token
    {
        get
        {
            return this.getCookie(cookieName_office365token);
        }
        set
        {
            this.setCookie(cookieName_office365token, value);
        }
    }
    


    private void setCookie(string key, string value){
        // set it to the local cache incase we need to read it later
        this.localResponseCookiesTemp[key] = value;

        httpContext.Response.Cookies.Append(key, value);
    }
    
    private string getCookie(string key){
        // if we set a cookie, read it first from the local cookie cache
        if(this.localResponseCookiesTemp.ContainsKey(key)){
            return this.localResponseCookiesTemp[key];
        }

        if (httpContext.Request.Cookies.TryGetValue(key, out string cookieValue))
        {
            return cookieValue;
        }

        return null;
    }
}
﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspnetCore_MicrosoftLoginWithoutLibraryHelp;



[ApiController]
[Route("api/[controller]")]
public class GeneralController : ControllerBase
{
    private static nac.Logging.Logger log = new();

    [HttpGet, Route("Hello")]
    public async Task<ContentResult> GetHello()
    {
        repositories.MicrosoftLogin.RedirectIfNotLoggedIn(HttpContext);

        var cookieHandler = new repositories.LocalCookiesRepo(HttpContext);
        
        var user = await repositories.GraphAPIRepo.GetUser(token: cookieHandler.office365token);

        return Content($"Hello World! {user}", "application/text");
    }


    [HttpGet, Route("msLogin")]
    public async Task<IActionResult> LoginWithOffice365Code([FromQuery]string code,
                    [FromQuery]string state 
    ){
        log.Info($"Received code from office365: {code}");

        // state will be our base64 encoded data
        var stateObj = repositories.MicrosoftLogin.ReadState(stateBase64Encoded: state);

        string token = await repositories.MicrosoftLogin.GetTokenFromCode(code: code,
                                                originalUrlCodeObtainedFor: stateObj.urlCodeObtainedFor);
        log.Info($"Received Token: {token}");

        var cookieHandler = new repositories.LocalCookiesRepo(HttpContext);
        cookieHandler.office365token = token; // set the cookie then redirect back to who called us
        

        return Redirect(stateObj.OriginalUrl);
    }
    
    
}



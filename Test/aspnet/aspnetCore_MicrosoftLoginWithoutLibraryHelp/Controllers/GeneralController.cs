using System;
using Microsoft.AspNetCore.Mvc;

namespace aspnetCore_MicrosoftLoginWithoutLibraryHelp;



[ApiController]
[Route("api/[controller]")]
public class GeneralController : ControllerBase
{
    private static nac.Logging.Logger log = new();

    [HttpGet, Route("Hello")]
    public ContentResult GetHello()
    {
        repositories.MicrosoftLogin.RedirectIfNotLoggedIn(HttpContext);

        return Content("Hello World!", "application/text");
    }


    [HttpGet, Route("msLogin")]
    public async void LoginWithOffice365Code([FromQuery]string code,
                    [FromQuery]string state 
    ){
        log.Info($"Received code from office365: {code}");

        // state will be our base64 encoded data
        var stateObj = repositories.MicrosoftLogin.ReadState(stateBase64Encoded: state);

        string token = await repositories.MicrosoftLogin.GetTokenFromCode(code: code,
                                                originalUrlCodeObtainedFor: stateObj.urlCodeObtainedFor);
        log.Info($"Received Token: {token}");
    }
    
    
}



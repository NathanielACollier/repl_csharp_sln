using System;
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

        return Content("Hello World!", "application/text");
    }


    [HttpGet, Route("msLogin")]
    public async Task<IResult> LoginWithOffice365Code([FromQuery]string code,
                    [FromQuery]string state 
    ){
        log.Info($"Received code from office365: {code}");

        // state will be our base64 encoded data
        var stateObj = repositories.MicrosoftLogin.ReadState(stateBase64Encoded: state);

        string token = await repositories.MicrosoftLogin.GetTokenFromCode(code: code,
                                                originalUrlCodeObtainedFor: stateObj.urlCodeObtainedFor);
        log.Info($"Received Token: {token}");

        HttpContext.Response.Redirect(stateObj.OriginalUrl);
    }
    
    
}



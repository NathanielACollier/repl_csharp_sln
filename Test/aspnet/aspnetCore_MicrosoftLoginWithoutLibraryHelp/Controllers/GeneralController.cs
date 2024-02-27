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
    public void LoginWithOffice365Code([FromQuery]string code){
        log.Info($"Received code from office365: {code}");
    }
    
    
}



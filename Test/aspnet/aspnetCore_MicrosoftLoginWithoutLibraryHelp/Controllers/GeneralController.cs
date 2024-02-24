using System;
using Microsoft.AspNetCore.Mvc;

namespace aspnetCore_MicrosoftLoginWithoutLibraryHelp;



[ApiController]
[Route("api/[controller]")]
public class GeneralController : ControllerBase
{


    [HttpGet, Route("Hello")]
    public ContentResult GetHello()
    {
        return Content("Hello World!", "application/text");
    }
    
    
}



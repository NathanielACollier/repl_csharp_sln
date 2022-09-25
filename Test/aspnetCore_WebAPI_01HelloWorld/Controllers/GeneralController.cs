using System;
using Microsoft.AspNetCore.Mvc;

namespace aspnetCore_WebAPI_01HelloWorld.Controllers;

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


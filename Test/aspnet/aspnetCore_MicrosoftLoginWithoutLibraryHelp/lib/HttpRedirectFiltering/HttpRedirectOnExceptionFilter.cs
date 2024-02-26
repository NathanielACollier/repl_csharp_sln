using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace aspnetCore_MicrosoftLoginWithoutLibraryHelp.lib.HttpRedirectFiltering;

public class HttpRedirectOnExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order {get; set; } = int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is HttpRedirectException exception)
        {
            context.Result = new RedirectResult(url: exception.Url);
            context.ExceptionHandled = true;
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using tp7518.Sys;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // https://learn.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-7.0

        if (context.Exception != null)
        {
            var statusCode = context.Exception is BusinessException ? 
                StatusCodes.Status400BadRequest : 
                StatusCodes.Status500InternalServerError;

            context.Result = new ObjectResult(new 
            {
                Message = context.Exception.Message,        
            })
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }        
    }
}
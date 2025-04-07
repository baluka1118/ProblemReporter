using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProblemReporter.Helpers;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var error = new ErrorModel(context.Exception.Message);

        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Result = new JsonResult(error);
    }
}
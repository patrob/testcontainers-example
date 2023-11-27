using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestcontainersExample.Web.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
        }
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
}

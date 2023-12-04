using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestcontainersExample.Web.Filters;

namespace TestcontainersExample.Web.Controllers;

[Route("api/[controller]")]
[ApiExceptionFilter]
public class ApiControllerBase : Controller
{
    private IMediator? _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
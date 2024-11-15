using Microsoft.AspNetCore.Mvc;
using TestcontainersExample.Web.Filters;

namespace TestcontainersExample.Web.Controllers;

[Route("api/[controller]")]
[ApiExceptionFilter]
public class ApiControllerBase : Controller
{
}
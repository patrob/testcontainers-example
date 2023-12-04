using Microsoft.AspNetCore.Mvc;
using TestcontainersExample.Core.Features.Commands;
using TestcontainersExample.Core.Features.Queries;

namespace TestcontainersExample.Web.Controllers;

public class UsersController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    { 
        var response = await Mediator.Send(new GetUsersQuery());
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddUserCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await Mediator.Send(new GetUserByIdQuery { Id = id });
        return Ok(response);
    }
}
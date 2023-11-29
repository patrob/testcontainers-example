using Microsoft.AspNetCore.Mvc;
using TestcontainersExample.Core.Features.Commands;
using TestcontainersExample.Core.Features.Queries;

namespace TestcontainersExample.Web.Controllers;

public class PostsController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddPostCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, int pageSize = 10, string? sortBy = null, bool isDescending = false)
    {
        var response = await Mediator.Send(new GetPostsQuery
        {
            PageNumber = page,
            PageSize = pageSize,
            ColumnName = sortBy,
            IsDescending = isDescending
        });
        
        return Ok(response);
    }
}
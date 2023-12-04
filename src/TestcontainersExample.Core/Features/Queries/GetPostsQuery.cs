using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestcontainersExample.Core.Common.Extensions;
using TestcontainersExample.Core.Common.Interfaces;
using TestcontainersExample.Core.Common.Models;
using TestcontainersExample.Core.Dtos;
using TestcontainersExample.Core.Entities;

namespace TestcontainersExample.Core.Features.Queries;

public class GetPostsQuery : IRequest<PagedResult<PostDto>>
{
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
    public string? ColumnName { get; init; } = null;
    public bool IsDescending { get; init; } = true;
}

public class GetPostsQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetPostsQuery, PagedResult<PostDto>>
{
    public async Task<PagedResult<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await context.Posts
            .Include(x => x.User)
            .Sort(request.ColumnName ?? nameof(Post.Id), request.IsDescending)
            .Page(request.PageNumber, request.PageSize)
            .ToListAsync(cancellationToken);
        
        return new PagedResult<PostDto>
        {
            Items = mapper.Map<List<PostDto>>(posts),
            TotalCount = await context.Posts.CountAsync(cancellationToken)
        };
    }
}
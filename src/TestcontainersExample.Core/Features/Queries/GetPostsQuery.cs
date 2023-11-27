using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestcontainersExample.Core.Common.Interfaces;
using TestcontainersExample.Core.Dtos;

namespace TestcontainersExample.Core.Features.Queries;

public class GetPostsQuery : IRequest<List<PostDto>>
{
}

public class GetPostsQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetPostsQuery, List<PostDto>>
{
    public async Task<List<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await context.Posts.ToListAsync(cancellationToken);
        return mapper.Map<List<PostDto>>(posts);
    }
}
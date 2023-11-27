using AutoMapper;
using MediatR;
using TestcontainersExample.Core.Common.Interfaces;
using TestcontainersExample.Core.Entities;

namespace TestcontainersExample.Core.Features.Commands;

public class AddPostCommand : IRequest
{
    public required string Title { get; set; }
    public required string Body { get; set; }
    public required Guid UserId { get; set; }
}

public class AddPostCommandHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<AddPostCommand>
{
    public async Task Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        var post = mapper.Map<Post>(request);
        context.Posts.Add(post);
        await context.SaveChangesAsync(cancellationToken);
    }
}
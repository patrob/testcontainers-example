using MediatR;
using TestcontainersExample.Core.Common.Interfaces;

namespace TestcontainersExample.Core.Features.Commands;

public class DeletePostCommand : IRequest
{
    public required Guid Id { get; set; }
}

public class DeletePostCommandHandler(IApplicationDbContext context) : IRequestHandler<DeletePostCommand>
{
    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await context.Posts.FindAsync(request.Id);
        context.Posts.Remove(post!);
        await context.SaveChangesAsync(cancellationToken);
    }
}
using AutoMapper;
using MediatR;
using TestcontainersExample.Core.Common.Interfaces;
using TestcontainersExample.Core.Entities;

namespace TestcontainersExample.Core.Features.Commands;

public class AddUserCommand : IRequest
{
    public required string Name { get; set; }
}

public class AddUserCommandHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<AddUserCommand>
{
    public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
    }
}
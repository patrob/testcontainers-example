using AutoMapper;
using MediatR;
using TestcontainersExample.Core.Common.Interfaces;
using TestcontainersExample.Core.Dtos;

namespace TestcontainersExample.Core.Features.Queries;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public required Guid Id { get; init; }
}

public class GetUserByIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.Id, cancellationToken);
        return mapper.Map<UserDto>(user);
    }
}
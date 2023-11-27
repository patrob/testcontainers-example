using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestcontainersExample.Core.Common.Interfaces;
using TestcontainersExample.Core.Dtos;

namespace TestcontainersExample.Core.Features.Queries;

public class GetUsersQuery : IRequest<List<UserDto>>
{
}

public class GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await context.Users.ToListAsync(cancellationToken);
        return mapper.Map<List<UserDto>>(users);
    }
}
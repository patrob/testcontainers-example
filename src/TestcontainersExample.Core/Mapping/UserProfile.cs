using AutoMapper;
using TestcontainersExample.Core.Dtos;
using TestcontainersExample.Core.Entities;
using TestcontainersExample.Core.Features.Commands;

namespace TestcontainersExample.Core.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ReverseMap();
        CreateMap<AddUserCommand, User>();
    }
}
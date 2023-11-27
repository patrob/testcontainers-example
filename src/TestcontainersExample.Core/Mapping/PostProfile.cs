using AutoMapper;
using TestcontainersExample.Core.Dtos;
using TestcontainersExample.Core.Entities;
using TestcontainersExample.Core.Features.Commands;

namespace TestcontainersExample.Core.Mapping;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<AddPostCommand, Post>();
        CreateMap<PostDto, Post>()
            .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(x => x.User, opt => opt.Ignore());
        CreateMap<Post, PostDto>();

    }
}
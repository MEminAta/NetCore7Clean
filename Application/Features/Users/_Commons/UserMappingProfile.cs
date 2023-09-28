using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Login;
using Application.Features.Users.Commands.RefreshToken;
using AutoMapper;
using Domain.Derived;
using Security.Token.Models;

namespace Application.Features.Users._Commons;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserCreateCommand, User>()
            .ForMember(x => x.Password,
                y => y.Ignore());
        CreateMap<User, UserCreatedModel>();
        CreateMap<User, UserLoggedModel>();
        CreateMap<List<ModuleIdWithPermissionOrders>, UserRefreshAccessToken>();
    }
}
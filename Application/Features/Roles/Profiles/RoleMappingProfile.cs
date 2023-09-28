using Application.Features.Roles.Commands.Create;
using Application.Features.Roles.Commands.Create.Dtos;
using Application.Features.Roles.Queries.GetList.Dtos;
using AutoMapper;
using Domain.Derived;

namespace Application.Features.Roles.Profiles;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<Role, RoleCreatedDto>().ReverseMap();
        CreateMap<Role, RoleCreateCommand>().ReverseMap();
        CreateMap<Role, RoleListDto>().ReverseMap();
    }
}
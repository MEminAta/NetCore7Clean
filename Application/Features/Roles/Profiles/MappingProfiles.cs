using Application.Features.Roles.Commands.Create;
using Application.Features.Roles.Queries.GetList;
using Application.Paging;
using AutoMapper;
using Domain.Derived;

namespace Application.Features.Roles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Role, RoleCreatedDto>().ReverseMap();
        CreateMap<Role, RoleCreateCommand>().ReverseMap();
        CreateMap<IPaginate<Role>, RoleListModel>().ReverseMap();
        CreateMap<Role, RoleListDto>().ReverseMap();
    }
}
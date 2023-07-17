using Application.Features.Roles.Commands.Create;
using Application.Features.Roles.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Roles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Role, CreatedRoleDto>().ReverseMap();
        CreateMap<Role, CreateRoleCommand>().ReverseMap();
    }
}
using Application.Features.Roles.Rules;
using Application.IRepositories.Derived;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Roles.Commands.Create;

public class RoleCreateCommand : IRequest<RoleCreatedDto>
{
    public RoleCreateCommand(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public class RoleCreateCommandHandler : IRequestHandler<RoleCreateCommand, RoleCreatedDto>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly RoleBusinessRules _roleBusinessRules;

        public RoleCreateCommandHandler(IRoleRepository roleRepository, IMapper mapper,
            RoleBusinessRules roleBusinessRules)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _roleBusinessRules = roleBusinessRules;
        }

        public async Task<RoleCreatedDto> Handle(RoleCreateCommand request, CancellationToken ct)
        {
            await _roleBusinessRules.RoleNameCanNotBeDuplicatedWhenInserted(request.Name);

            var mappedRole = _mapper.Map<Role>(request);
            var createdRole = await _roleRepository.Add(mappedRole, ct);
            var createdRoleDto = _mapper.Map<RoleCreatedDto>(createdRole);
            return createdRoleDto;
        }
    }
}
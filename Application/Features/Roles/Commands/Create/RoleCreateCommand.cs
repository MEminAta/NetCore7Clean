using Application.Features.Roles.Commands.Create.Dtos;
using Application.Features.Roles.Rules;
using Application.Repository.Contexts;
using AutoMapper;
using Domain.Derived;
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
        private readonly BaseDbContext _context;
        private readonly IMapper _mapper;
        private readonly RoleBusinessRules _roleBusinessRules;

        public RoleCreateCommandHandler(BaseDbContext context, IMapper mapper, RoleBusinessRules roleBusinessRules)
        {
            _context = context;
            _mapper = mapper;
            _roleBusinessRules = roleBusinessRules;
        }

        public async Task<RoleCreatedDto> Handle(RoleCreateCommand request, CancellationToken ct)
        {
            await _roleBusinessRules.RoleNameCanNotBeDuplicatedWhenInserted(request.Name, ct);

            var role = _mapper.Map<Role>(request);
            await _context.Roles.AddAsync(role, ct);
            await _context.SaveChangesAsync(ct);
            var createdRoleDto = _mapper.Map<RoleCreatedDto>(role);
            return createdRoleDto;
        }
    }
}
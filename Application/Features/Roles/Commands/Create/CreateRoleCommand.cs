using Application.AOP.Aspects;
using Application.Features.Roles.DTOs;
using Application.Features.Roles.Rules;
using Application.IRepositories.EntityRepositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Roles.Commands.Create;

public class CreateRoleCommand : IRequest<CreatedRoleDto>
{
    public CreateRoleCommand(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreatedRoleDto>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly RoleBusinessRules _roleBusinessRules;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper,
            RoleBusinessRules roleBusinessRules)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _roleBusinessRules = roleBusinessRules;
        }

        [PerformanceAspect]
        public virtual async Task<CreatedRoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            // await _roleBusinessRules.RoleNameCanNotBeDuplicatedWhenInserted(request.Name);

            var mappedRole = _mapper.Map<Role>(request);
            var createdRole = await _roleRepository.Add(mappedRole);
            var createdRoleDto = _mapper.Map<CreatedRoleDto>(createdRole);
            return createdRoleDto;
        }
    }
}

public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        Console.WriteLine("selam1");
        // Code to execute before command handling
        var response = await next();
        Console.WriteLine("selam2");

        // Code to execute after command handling

        return response;
    }
}
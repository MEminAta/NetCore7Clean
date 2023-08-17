using Application.DynamicQuery;
using Application.IRepositories.Derived;
using AutoMapper;
using MediatR;

namespace Application.Features.Roles.Queries.GetDynamic;

public class RoleGetDynamicQuery : IRequest<object>
{
    public Dynamic Dynamic { get; set; }

    public class RoleGetDynamicQueryHandler : IRequestHandler<RoleGetDynamicQuery, object>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleGetDynamicQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public Task<object> Handle(RoleGetDynamicQuery request, CancellationToken cancellationToken)
        {
            var x = _roleRepository.GetListByDynamic(
                dynamic: request.Dynamic,
                ct: cancellationToken
            );

            return Task.FromResult(new object());
        }
    }
}
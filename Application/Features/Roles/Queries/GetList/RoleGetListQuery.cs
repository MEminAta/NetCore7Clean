using Application.IRepositories.Derived;
using Application.Paging;
using AutoMapper;
using MediatR;

namespace Application.Features.Roles.Queries.GetList;

public class RoleGetListQuery : BasePageableRequest, IRequest<RoleListModel>
{
    public class GetListRoleQueryHandler : IRequestHandler<RoleGetListQuery, RoleListModel>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetListRoleQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleListModel> Handle(RoleGetListQuery request, CancellationToken ct)
        {
            var roles = await _roleRepository.GetList(index: request.Page, size: request.PageSize, ct: ct);
            var mappedRoleListModel = _mapper.Map<RoleListModel>(roles);
            return mappedRoleListModel;
        }
    }
}
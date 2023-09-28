using Application.Features.Roles.Queries.GetList.Dtos;
using Application.Repository.Contexts;
using Application.Repository.Extensions.Paging;
using Application.Repository.Extensions.Paging.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Roles.Queries.GetList;

public class RoleGetListQuery : IRequest<OffsetPaginatedModel<RoleListDto>>
{
    public class GetListRoleQueryHandler : IRequestHandler<RoleGetListQuery, OffsetPaginatedModel<RoleListDto>>
    {
        private readonly IMapper _mapper;
        private readonly BaseDbContext _context;

        public GetListRoleQueryHandler(IMapper mapper, BaseDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<OffsetPaginatedModel<RoleListDto>> Handle(RoleGetListQuery request, CancellationToken ct)
        {
            var paginatedRole = await _context.Roles
                .Where(x => x.IsDeleted)
                .Select(x => _mapper.Map<RoleListDto>(x))
                .ToOffsetPaginate(10, 0);
            return paginatedRole;
        }
    }
}
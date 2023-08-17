using Application.Paging;

namespace Application.Features.Roles.Queries.GetList;

public class RoleListModel : BasePageableResponse
{
    public List<RoleListDto> Items { get; set; } = new();
}
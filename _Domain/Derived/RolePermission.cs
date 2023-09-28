using Domain.Bases;

namespace Domain.Derived;

public class RolePermission : BaseEntity<int>
{
    public required int RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public required int PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;

}
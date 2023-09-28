using Domain.Bases;

namespace Domain.Derived;

public class Module : BaseSystemEntity
{
    public required string Name { get; set; }
    public virtual IEnumerable<Permission> Permissions { get; set; } = null!;
    public virtual IEnumerable<RolePermission> RolePermissions { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Bases;

namespace Domain.Derived;

public class Role : BaseEntity<int>
{
    public required string Name { get; set; }
    public virtual IEnumerable<RolePermission> RolePermissions { get; set; } = null!;
    [InverseProperty(nameof(Role))] public virtual IEnumerable<User> Users { get; set; } = null!;
}
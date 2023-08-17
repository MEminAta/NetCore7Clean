using Domain.Bases;

namespace Domain.Entities;

public class Permission : BaseEntity<int>
{
    public string Name { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
}
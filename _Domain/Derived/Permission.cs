using Domain.Bases;

namespace Domain.Derived;

public class Permission : BaseEntityWithEvent<int>
{
    public string Name { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }

    public int ModuleId { get; set; }
    public Module Module { get; set; }
}
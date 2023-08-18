using Domain.Bases;

namespace Domain.Derived;

public class Module : BaseEntity<int>
{
    public string Name { get; set; }
    public IEnumerable<Permission> Permissions { get; set; }
}
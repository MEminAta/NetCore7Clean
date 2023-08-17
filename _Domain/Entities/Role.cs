using Domain.Bases;

namespace Domain.Entities;

public class Role : BaseEntity<int>
{
    public string Name { get; set; }
    public IEnumerable<Permission> Permissions { get; set; }
    public IEnumerable<User> Users { get; set; }
}
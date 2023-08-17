using Domain.Bases;

namespace Domain.Entities;

public class User : BaseEntity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] Password { get; set; }
    public byte[] Salt { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }
}
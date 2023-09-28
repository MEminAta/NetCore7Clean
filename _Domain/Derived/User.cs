using System.ComponentModel.DataAnnotations;
using Domain.Bases;

namespace Domain.Derived;

public class User : BaseEntity<int>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required byte[] Password { get; set; }
    public required byte[] Salt { get; set; }
    public string? RefreshToken { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
using Domain.Bases;

namespace Domain.Derived;

public class LoginLog : BaseSystemEntity
{
    public required string IpAddress { get; set; }
    public required string? Device { get; set; }
    public required string? UserAgent { get; set; }
    public required int UserId { get; set; }
}
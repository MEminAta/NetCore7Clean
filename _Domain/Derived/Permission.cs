using Domain.Bases;

namespace Domain.Derived;

public class Permission : BaseSystemEntity
{
    public required string Name { get; set; }

    public required int ModuleId { get; set; }
    public Module Module { get; set; } = null!;
    public required int Order { get; set; }
}
namespace Security.Token.Models;

public class ModuleIdWithPermissionOrders
{
    public required int ModuleId { get; set; }
    public required List<int> PermissionOrders { get; set; }
}
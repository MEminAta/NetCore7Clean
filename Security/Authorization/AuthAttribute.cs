using Microsoft.AspNetCore.Mvc.Filters;
using Security.Compress;
using Security.Token.Models;

namespace Security.Authorization;

[AttributeUsage(AttributeTargets.Method)]
public class AuthAttribute : Attribute, IAuthorizationFilter
{
    private readonly int _moduleId;
    private readonly int[] _permissionOrders;

    public AuthAttribute(ModuleEnum moduleId, params int[] permissionOrders)
    {
        Array.Sort(permissionOrders);
        Array.Reverse(permissionOrders);
        _moduleId = (int)moduleId;
        _permissionOrders = permissionOrders;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var moduleId = context.HttpContext.User.Claims.First(x => x.Type.Equals(nameof(AccessTokenModel.MId).ToLowerInvariant())).Value;
        if (Convert.ToInt32(moduleId) != _moduleId) throw new Exception("Wrong Access Token.");

        var compressedPOs = context.HttpContext.User.Claims.First(x => x.Type.Equals(nameof(AccessTokenModel.POs).ToLowerInvariant())).Value;
        var bitPOs = SortedNumberCompressor.Decompress(compressedPOs);

        if (_permissionOrders.Where(pOs => bitPOs.Length >= pOs).Any(pOs => bitPOs[pOs - 1]))
            return;

        throw new UnauthorizedAccessException();
    }
}
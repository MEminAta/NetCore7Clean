using Domain.Derived;
using Microsoft.EntityFrameworkCore;
using Security.Token.Models;

namespace Application.Features.RolePermissions.Extensions.GetAccessTokens;

public static class GetAccessTokensExtension
{
    public static async Task<List<ModuleIdWithPermissionOrders>> GetAccessTokens(this IQueryable<RolePermission> context, CancellationToken ct)
    {
        var data = await context
            .Select(x => x.Permission)
            .GroupBy(x => x.ModuleId)
            .Select(x => new ModuleIdWithPermissionOrders { ModuleId = x.Key, PermissionOrders = x.Select(y => y.Order).ToList() })
            .ToListAsync(ct);
        return data;
    }
}
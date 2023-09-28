using Domain.Derived;
using Microsoft.EntityFrameworkCore;
using Security.Token.Models;

namespace Application.Features.Users.Extensions.GetAccessTokens;

public static class GetAccessTokensExtension
{
    // public static async Task<List<ModuleIdWithPermissionOrders>> GetAccessTokens(this DbSet<User> context, int userId, CancellationToken ct)
    // {
    //     var data = await context.Where(x => x.Id == userId)
    //
    //
    //     // var data = await context.Where(x => x.RoleId == roleId)
    //     //     .Select(x => x.Permission)
    //     //     .GroupBy(x => x.ModuleId)
    //     //     .Select(x => new ModuleIdWithPermissionOrders { ModuleId = x.Key, PermissionOrders = x.Select(y => y.Order).ToList() })
    //     //     .ToListAsync(ct);
    //     return data;
    // }
}
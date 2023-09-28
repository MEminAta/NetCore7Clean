using Application.Repository.Contexts;
using CrossCuttingConcern.Exceptions.ExceptionTypes;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Roles.Rules;

public class RoleBusinessRules
{
    private readonly BaseDbContext _context;

    public RoleBusinessRules(BaseDbContext context)
    {
        _context = context;
    }

    public async Task RoleNameCanNotBeDuplicatedWhenInserted(string name, CancellationToken ct)
    {
        var result = await _context.Roles.FirstOrDefaultAsync(x => x.Name == name, cancellationToken: ct);
        if (result != null)
            throw new RuleException("Role name exists.");
    }
}
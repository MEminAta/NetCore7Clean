using Application.Repository.Contexts;
using CrossCuttingConcern.Exceptions.ExceptionTypes;
using CrossCuttingConcern.Globalization;
using CrossCuttingConcern.Globalization.Constants;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users._Commons;

public class UserBusinessRules
{
    private readonly BaseDbContext _context;

    public UserBusinessRules(BaseDbContext context)
    {
        _context = context;
    }

    public async Task UserEmailCanNotDuplicated(string email, CancellationToken ct)
    {
        var result = await _context.Users.AnyAsync(x => x.Email == email, cancellationToken: ct);
        if (result) throw new RuleException(LocalizationManager.GetString(ResourceKeys.UserEmailCanNotDuplicated));
    }
}
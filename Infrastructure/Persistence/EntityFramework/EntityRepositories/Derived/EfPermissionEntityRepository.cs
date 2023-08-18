using Application.IRepositories.Derived;
using Domain.Derived;
using Infrastructure.Persistence.EntityFramework.Contexts;
using Infrastructure.Persistence.EntityFramework.EntityRepositories.Bases;

namespace Infrastructure.Persistence.EntityFramework.EntityRepositories.Derived;

public class EfPermissionEntityRepository : EfBaseEntityRepository<Permission, EfDbContext>, IPermissionRepository
{
    public EfPermissionEntityRepository(EfDbContext context) : base(context)
    {
    }
}
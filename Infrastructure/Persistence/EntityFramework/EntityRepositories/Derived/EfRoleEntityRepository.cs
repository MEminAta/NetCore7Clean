using Application.IRepositories.Derived;
using Domain.Derived;
using Infrastructure.Persistence.EntityFramework.Contexts;
using Infrastructure.Persistence.EntityFramework.EntityRepositories.Bases;

namespace Infrastructure.Persistence.EntityFramework.EntityRepositories.Derived;

public class EfRoleEntityRepository : EfBaseEntityRepository<Role, EfDbContext>, IRoleRepository
{
    public EfRoleEntityRepository(EfDbContext context) : base(context)
    {
    }
}
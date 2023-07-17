using Application.IRepositories.EntityRepositories;
using Domain.Entities;
using Infrastructure.Persistence.EntityFramework.Contexts;

namespace Infrastructure.Persistence.EntityFramework.Repositories.EntityRepositories;

public class EfRoleEntityRepository : EfBaseEntityRepository<Role, EfDbContext>, IRoleRepository
{
    public EfRoleEntityRepository(EfDbContext context) : base(context)
    {
    }
}
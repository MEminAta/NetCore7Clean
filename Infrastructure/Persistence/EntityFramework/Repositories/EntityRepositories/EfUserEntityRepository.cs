using Application.IRepositories.EntityRepositories;
using Domain.Entities;
using Infrastructure.Persistence.EntityFramework.Contexts;

namespace Infrastructure.Persistence.EntityFramework.Repositories.EntityRepositories;

public class EfUserEntityRepository : EfBaseEntityRepository<User, EfDbContext>, IUserRepository
{
    public EfUserEntityRepository(EfDbContext context) : base(context)
    {
    }
}
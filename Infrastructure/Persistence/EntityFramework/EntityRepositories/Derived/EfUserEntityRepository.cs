using Application.IRepositories.Derived;
using Domain.Entities;
using Infrastructure.Persistence.EntityFramework.Contexts;
using Infrastructure.Persistence.EntityFramework.EntityRepositories.Bases;

namespace Infrastructure.Persistence.EntityFramework.EntityRepositories.Derived;

public class EfUserEntityRepository : EfBaseEntityRepository<User, EfDbContext>, IUserRepository
{
    public EfUserEntityRepository(EfDbContext context) : base(context)
    {
    }
}
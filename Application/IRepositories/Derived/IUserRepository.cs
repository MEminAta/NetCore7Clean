using Application.IRepositories.Bases;
using Domain.Derived;

namespace Application.IRepositories.Derived;

public interface IUserRepository : IBaseEntityRepository<User>
{
}
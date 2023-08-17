using Application.IRepositories.Bases;
using Domain.Entities;

namespace Application.IRepositories.Derived;

public interface IUserRepository : IBaseEntityRepository<User>
{
}
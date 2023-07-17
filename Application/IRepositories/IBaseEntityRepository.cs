using System.Linq.Expressions;
using Domain.Bases;

namespace Application.IRepositories;

public interface IBaseEntityRepository<T> where T : BaseEntity, new()
{
    public Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);
    public Task<T?> Get(Expression<Func<T, bool>> filter);
    public Task<bool> Any(Expression<Func<T, bool>> filter);
    public Task<T> Add(T entity);
    public Task Update(T entity);
    public Task Delete(T entity);
    public Task AdvancedUpdate(T entity, List<string> props);
    public IQueryable<T> GetIQueryable();
}
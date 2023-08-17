using System.Linq.Expressions;
using Application.DynamicQuery;
using Application.Paging;
using Domain.Bases;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.IRepositories.Bases;

public interface IBaseEntityRepository<TEntity> where TEntity : BaseEntity
{
    public Task<TEntity?> Get(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default);

    public Task<TEntityDto?> Get<TEntityDto>(
        Expression<Func<TEntity, TEntityDto>> select,
        Expression<Func<TEntityDto, bool>> predicate,
        CancellationToken ct = default);

    public Task<IPaginate<TEntity>> GetList(
        CancellationToken ct,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0, int size = 10, bool enableTracking = false);

    public Task<IPaginate<TEntityDto>> GetList<TEntityDto>(
        Expression<Func<TEntity, TEntityDto>> select,
        CancellationToken ct,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0, int size = 10, bool enableTracking = false);

    public Task<IPaginate<TEntity>> GetListByDynamic(
        Dynamic dynamic,
        CancellationToken ct,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0, int size = 10, bool enableTracking = false);

    public Task<bool> Any(Expression<Func<TEntity, bool>> filter, CancellationToken ct);
    public Task<TEntity> Add(TEntity entity, CancellationToken ct);
    public Task<TEntity> Update(TEntity entity, CancellationToken ct);
    public Task AdvancedUpdate(TEntity entity, List<string> props, CancellationToken ct);
    public Task<TEntity> Delete(TEntity entity, CancellationToken ct);
    public IQueryable<T> Query<T>(Expression<Func<TEntity, T>> select);
}
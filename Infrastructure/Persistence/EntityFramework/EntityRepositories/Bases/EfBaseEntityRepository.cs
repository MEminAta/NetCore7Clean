using System.Linq.Expressions;
using Application.DynamicQuery;
using Application.IRepositories.Bases;
using Application.Paging;
using Domain.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Persistence.EntityFramework.EntityRepositories.Bases;

public class EfBaseEntityRepository<TEntity, TContext> :
    IBaseEntityRepository<TEntity>
    where TEntity : BaseEntity
    where TContext : DbContext
{
    public TContext Context { get; }

    public EfBaseEntityRepository(TContext context)
    {
        Context = context;
    }

    public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate, ct);
    }

    public async Task<TEntityDto?> Get<TEntityDto>(Expression<Func<TEntity, TEntityDto>> select, Expression<Func<TEntityDto, bool>> predicate, CancellationToken ct = default)
    {
        return await Context.Set<TEntity>().Select(select).FirstOrDefaultAsync(predicate, ct);
    }

    public async Task<IPaginate<TEntity>> GetList(
        CancellationToken ct,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0, int size = 10, bool enableTracking = false)
    {
        IQueryable<TEntity> queryable = Context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null)
            return await orderBy(queryable).ToPaginateAsync(index, size, 0, ct);
        return await queryable.ToPaginateAsync(index, size, 0, ct);
    }

    public async Task<IPaginate<T>> GetList<T>(
        Expression<Func<TEntity, T>> select,
        CancellationToken ct,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0, int size = 10, bool enableTracking = false)
    {
        IQueryable<TEntity> queryable = Context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null) queryable = orderBy(queryable);
        return await queryable.Select(select).ToPaginateAsync(index, size, 0, ct);
    }

    public async Task<IPaginate<TEntity>> GetListByDynamic(
        Dynamic dynamic,
        CancellationToken cancellationToken,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0, int size = 10,
        bool enableTracking = false)
    {
        var queryable = Context.Set<TEntity>().AsQueryable().ToDynamic(dynamic);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> filter, CancellationToken ct = default)
    {
        return await Context.Set<TEntity>().AnyAsync(filter, ct);
    }

    public async Task<TEntity> Add(TEntity entity, CancellationToken ct)
    {
        Context.Entry(entity).State = EntityState.Added;
        await Context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<TEntity> Update(TEntity entity, CancellationToken ct)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task AdvancedUpdate(TEntity entity, List<string> props, CancellationToken ct)
    {
        var advancedUpdatedEntity = Context.Attach(entity);

        foreach (var prop in props)
            advancedUpdatedEntity.Property(prop).IsModified = true;

        await Context.SaveChangesAsync(ct);
    }

    public async Task<TEntity> Delete(TEntity entity, CancellationToken ct)
    {
        Context.Entry(entity).State = EntityState.Deleted;
        await Context.SaveChangesAsync(ct);
        return entity;
    }

    public IQueryable<T> Query<T>(Expression<Func<TEntity, T>> select)
    {
        return Context.Set<TEntity>().Select(select);
    }
}
using System.Linq.Expressions;
using Application.IRepositories;
using Domain.Bases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityFramework.Repositories;

public class EfBaseEntityRepository<TEntity, TContext> :
    IBaseEntityRepository<TEntity>
    where TEntity : BaseEntity, new()
    where TContext : DbContext
{
    private TContext Context { get; }

    public EfBaseEntityRepository(TContext context)
    {
        Context = context;
    }

    public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> filter)
    {
        return await Context.Set<TEntity>().SingleOrDefaultAsync(filter);
    }

    public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null)
    {
        return filter == null
            ? await Context.Set<TEntity>().ToListAsync()
            : await Context.Set<TEntity>().Where(filter).ToListAsync();
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> filter)
    {
        return await Context.Set<TEntity>().AnyAsync(filter);
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        var addedEntity = Context.Entry(entity);
        addedEntity.State = EntityState.Added;
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task Update(TEntity entity)
    {
        var updatedEntity = Context.Entry(entity);
        updatedEntity.State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public async Task AdvancedUpdate(TEntity entity, List<string> props)
    {
        var advancedUpdatedEntity = Context.Attach(entity);

        foreach (var prop in props)
            advancedUpdatedEntity.Property(prop).IsModified = true;

        await Context.SaveChangesAsync();
    }

    public async Task Delete(TEntity entity)
    {
        var deletedEntity = Context.Entry(entity);
        deletedEntity.State = EntityState.Deleted;
        await Context.SaveChangesAsync();
    }

    public IQueryable<TEntity> GetIQueryable()
    {
        return Context.Set<TEntity>();
    }
}
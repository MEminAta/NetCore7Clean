using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Repository.Extensions.Updating;

public static class AdvancedUpdating
{
    public static EntityEntry<T> AdvancedUpdate<T>(this DbSet<T> dbSet, T entity, List<string> props) where T : class, new()
    {
        var entityEntry = dbSet.Attach(entity);

        foreach (var prop in props)
            entityEntry.Property(prop).IsModified = true;

        return entityEntry;
    }

    public static EntityEntry AdvancedUpdate<T>(this DbContext context, T entity, List<string> props) where T : notnull
    {
        var entityEntry = context.Attach(entity);
        foreach (var prop in props)
            entityEntry.Property(prop).IsModified = true;

        return entityEntry;
    }

    public static void AdvancedUpdateRange<T>(this DbSet<T> dbSet, List<T> entities, List<string> props) where T : class, new()
    {
        foreach (var entity in entities)
        {
            var entityEntry = dbSet.Attach(entity);

            foreach (var prop in props)
            {
                entityEntry.Property(prop).IsModified = true;
            }
        }
    }

    public static void AdvancedUpdateRange<T>(this DbContext context, List<T> entities, List<string> props) where T : class, new()
    {
        foreach (var entity in entities)
        {
            var entityEntry = context.Attach(entity);

            foreach (var prop in props)
            {
                entityEntry.Property(prop).IsModified = true;
            }
        }
    }
}
using Domain.Entities;
using Infrastructure.Persistence.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.EntityFramework.Contexts;

public class EfDbContext : DbContext
{
    public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EfRoleConfiguration());
        modelBuilder.ApplyConfiguration(new EfUserConfiguration());
        base.OnModelCreating(modelBuilder); // this thing is useless
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(x => x.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;
}
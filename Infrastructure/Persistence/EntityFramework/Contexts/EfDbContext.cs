using Domain.Entities;
using Infrastructure.Persistence.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.EntityFramework.Contexts;

public class EfDbContext : DbContext
{
    private readonly IConfiguration _config;

    public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EfUserConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("Sql"));
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
}
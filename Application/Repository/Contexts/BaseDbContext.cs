using Domain.Bases;
using Domain.Derived;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Security.Authorization;
using Security.Time;
using Security.Token.Models;

namespace Application.Repository.Contexts;

public class BaseDbContext : DbContext
{
    private readonly HttpContext _httpContext;

    public BaseDbContext(DbContextOptions<BaseDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContext = httpContextAccessor.HttpContext!;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var foreignKey in entityType.GetForeignKeys())
            {
                foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }

        var time = new TimeService();

        const string salt = "5C9DB11118D41D7307B2EDEE3BE6140D1979E768E69FD5F7BCE5B3A59B8ED869C3EB0B6873AFDF5A820044F22E5E79AC20987BA2DCC248C5AF2034F81981D3CF";
        const string password = "5B38916EF68E391600913D005F0D38CC1D47BC07D2B88C6F8DCFB60F82D4A4E5";

        var passwordBytes = Enumerable.Range(0, password.Length).Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(password.Substring(x, 2), 16))
            .ToArray();

        var saltBytes = Enumerable.Range(0, salt.Length).Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(salt.Substring(x, 2), 16))
            .ToArray();

        var modules = new List<Module> { new() { Id = 1, Name = nameof(ModuleEnum.BaseModule) } };
        modelBuilder.Entity<Module>().HasData(modules);

        var permissions = new List<Permission>
        {
            new() { Id = 1, Name = nameof(BaseModulePermission.CreateUser), ModuleId = (int)ModuleEnum.BaseModule, Order = (int)BaseModulePermission.CreateUser },
            new() { Id = 2, Name = nameof(BaseModulePermission.GetUser), ModuleId = (int)ModuleEnum.BaseModule, Order = (int)BaseModulePermission.GetUser }
        };

        modelBuilder.Entity<Permission>().HasData(permissions);

        var roles = new List<Role> { new() { Id = 1, Name = "Admin", CreateTime = time.Initialize, UpdateTime = time.Initialize } };
        modelBuilder.Entity<Role>().HasData(roles);

        var rolePermissions = new List<RolePermission>
        {
            new() { Id = 1, PermissionId = 1, RoleId = 1, CreateTime = time.Initialize, UpdateTime = time.Initialize },
            new() { Id = 2, PermissionId = 2, RoleId = 1, CreateTime = time.Initialize, UpdateTime = time.Initialize }
        };
        modelBuilder.Entity<RolePermission>().HasData(rolePermissions);

        var users = new List<User> { new() { Id = 1, FirstName = "Emin", LastName = "Atasayar", Email = "emin@ata.com", Password = passwordBytes, Salt = saltBytes, RoleId = 1, CreateTime = time.Initialize, UpdateTime = time.Initialize } };
        modelBuilder.Entity<User>().HasData(users);


        base.OnModelCreating(modelBuilder); // this thing is useless
    }

    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        int? uId = null;
        var claim = _httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(nameof(AccessTokenModel.UId).ToLowerInvariant()));
        if (claim != null) uId = Convert.ToInt32(claim.Value);

        var entries = ChangeTracker.Entries()
            .Where(e => e is { Entity: BaseEntity, State: EntityState.Added or EntityState.Modified });

        var timeService = new TimeService();
        foreach (var entityEntry in entries)
        {
            var entity = (BaseEntity)entityEntry.Entity;
            entity.UpdateTime = timeService.Now;
            entity.UpdateUserId = uId;

            if (entityEntry.State != EntityState.Added) continue;
            entity.CreateUserId = uId;
            entity.CreateTime = timeService.Now;
        }

        return base.SaveChangesAsync(ct);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.EnableSensitiveDataLogging();
        // optionsBuilder.ConfigureWarnings(x => x.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<RolePermission> RolePermissions { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;
    public DbSet<Module> Modules { get; set; } = null!;
    public DbSet<LoginLog> LoginLogs { get; set; } = null!;
}
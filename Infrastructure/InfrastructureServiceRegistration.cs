using Infrastructure.Persistence.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Interface Implementations");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Gray;
        
        services.RegisterAssemblyPublicNonGenericClasses()
            .Where(x => Implementations(configuration, x.Name))
            .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

        services.AddDbContext<EfDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Sql"));
            }
        );
        return services;
    }

    private static bool Implementations(IConfiguration configuration, string name) =>
        RepositoryImplementations(configuration, name);

    private static bool RepositoryImplementations(IConfiguration configuration, string name)
    {
        if (!name.StartsWith(configuration["ORM"] ?? "Ef") || !name.EndsWith("EntityRepository")) return false;
        Console.WriteLine("ORM - " + name);

        return true;
    }
}
using System.Reflection;
using Application.Features.Roles.Rules;
using Microsoft.Extensions.DependencyInjection;

namespace Application.ServiceRegistrations;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(assembly);
        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

        services.AddScoped<RoleBusinessRules>();

        return services;
    }
}
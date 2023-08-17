using System.Reflection;
using Application.Features.Roles.Rules;
using Application.PipelineBehaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.ServiceRegistrations;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.AddAutoMapper(assembly);
        
        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        services.AddScoped<Mediator>();

        services.AddValidatorsFromAssembly(assembly);

        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TestPipeline<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        services.AddScoped<RoleBusinessRules>();

        return services;
    }
}
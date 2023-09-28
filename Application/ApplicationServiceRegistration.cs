using System.Reflection;
using Application.Features.Roles.Rules;
using Application.Features.Users._Commons;
using Application.Repository.Contexts;
using CrossCuttingConcern;
using CrossCuttingConcern.Validations;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Security.Compress;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseSqlServer(configuration.GetConnectionString("Sql"));
        });

        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);
        services.AddAutoMapper(typeof(UserMappingProfile));

        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        services.AddScoped<Mediator>();

        services.AddValidatorsFromAssembly(assembly);

        services.AddHttpContextAccessor();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));


        services.AddScoped<RoleBusinessRules>();
        services.AddScoped<UserBusinessRules>();

        services.AddSingleton<SortedNumberCompressor>();

        return services;
    }
}
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using MediatR;
using Microsoft.Extensions.Configuration;
using Module = Autofac.Module;

namespace Application.ServiceRegistrations.Autofac;

public class AutofacBusinessModule : Module
{
    private readonly IConfiguration _configuration;

    public AutofacBusinessModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Interface Implementations");

        builder
            .RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>))
            .EnableClassInterceptors(new ProxyGenerationOptions
            {
                Selector = new InterceptorSelector()
            })
            .InstancePerDependency();

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            if (_interfaceProjects.Any(x => assembly.GetName().Name == x))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(assembly.GetName().Name + " Module");

                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Gray;
                builder.RegisterAssemblyTypes(assembly)
                    .Where(x => Implementations(x.Name))
                    .AsImplementedInterfaces()
                    .EnableInterfaceInterceptors(new ProxyGenerationOptions
                    {
                        Selector = new InterceptorSelector()
                    }).InstancePerLifetimeScope();
            }
        }
    }

    private bool Implementations(string name) =>
        RepositoryImplementations(name);

    private bool RepositoryImplementations(string name)
    {
        if (!name.StartsWith(_configuration["ORM"] ?? "Ef") || !name.EndsWith("EntityRepository")) return false;
        Console.WriteLine(name);

        return true;
    }

    private static bool CommandHandlerImplementations(string name)
    {
        if (!name.EndsWith("CommandHandler")) return false;
        Console.WriteLine(name);
        return true;
    }

    private readonly List<string> _interfaceProjects = new() { "Infrastructure" };
    private readonly List<string> _classProjects = new() { "Application" };
}
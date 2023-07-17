using System.Reflection;
using Application.AOP.Bases;
using Castle.DynamicProxy;

namespace Application.ServiceRegistrations.Autofac;

public class InterceptorSelector : IInterceptorSelector
{
    public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
    {
        Console.WriteLine();
        Console.WriteLine(method.Name);
        Console.WriteLine(type.Name);
        Console.WriteLine();
        
        var attributes = new List<BaseInterception>();

        var classAttributes = type.GetCustomAttributes<BaseInterception>(true).ToList();
        var methodAttributes = type.GetMethod(method.Name)!.GetCustomAttributes<BaseInterception>(true);

        attributes.AddRange(classAttributes);
        attributes.AddRange(methodAttributes);

        // ReSharper disable once CoVariantArrayConversion
        return attributes.OrderBy(x => x.Priority).ToArray();
    }


    private static IEnumerable<BaseInterception> GetInterfaceAttributes(Type type, MemberInfo method)
    {
        var interfaces = type.GetInterfaces().FirstOrDefault(x => x.Name.EndsWith("Repository"));
        if (interfaces == null) return new List<BaseInterception>();
        var interfaceAttributes = interfaces.GetCustomAttributes<BaseInterception>(true).ToList();
        var interfaceMethodAttributes = interfaces.GetMethod(method.Name)!.GetCustomAttributes<BaseInterception>(false);
        interfaceAttributes.AddRange(interfaceMethodAttributes);

        return interfaceAttributes.ToList();
    }
}
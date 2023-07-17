using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;

namespace Demo;

internal abstract class Program
{
    private static void Main()
    {
        Console.Clear();

        var builder = new ContainerBuilder();

        builder.RegisterType<MyClass>()
            .EnableClassInterceptors(new ProxyGenerationOptions
            {
                Selector = new InterceptorSelector()
            });

        var container = builder.Build();
        var my = container.Resolve<MyClass>();
        Console.WriteLine(my.MyMethod());
    }
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class CallLogger : Attribute, IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine("Work");
        invocation.Proceed();
        invocation.ReturnValue = 88;
    }
}

public class MyClass
{
    [CallLogger]
    public virtual int MyMethod()
    {
        Console.WriteLine("Selam :D");
        return 142;
    }
}

public class InterceptorSelector : IInterceptorSelector
{
    public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
    {
        var attributes = new List<CallLogger>();

        var classAttributes = type.GetCustomAttributes<CallLogger>(true).ToList();
        var methodAttributes = type.GetMethod(method.Name)!.GetCustomAttributes<CallLogger>(true);

        attributes.AddRange(classAttributes);
        attributes.AddRange(methodAttributes);

        // ReSharper disable once CoVariantArrayConversion
        return attributes.ToArray();
    }
}
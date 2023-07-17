using Application.AOP.Types;
using Castle.DynamicProxy;

namespace Application.AOP.Aspects;

public class Test1Aspect<T> : InterceptionTResult<T> where T : new() 
{
    protected override void OnBefore(IInvocation invocation)
    {
        Console.WriteLine("OnBefore");
    }

    protected override void OnException(IInvocation invocation, Exception exception)
    {
        Console.WriteLine("OnException");
    }

    protected override void OnSuccess(IInvocation invocation, T result)
    {
        Console.WriteLine("OnSuccess");
        Console.WriteLine(result);
    }

    protected override void OnAfter(IInvocation invocation)
    {
        Console.WriteLine("OnAfter");
    }
}
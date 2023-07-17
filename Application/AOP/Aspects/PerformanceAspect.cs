using System.Diagnostics;
using Application.AOP.Bases;
using Castle.DynamicProxy;

namespace Application.AOP.Aspects;

public class PerformanceAspect : BaseInterception
{
    private Stopwatch _stopwatch;

    public PerformanceAspect()
    {
        _stopwatch = new Stopwatch();
    }

    public override void Intercept(IInvocation invocation)
    {
        _stopwatch.Start();
        Console.WriteLine("Start Watch");

        invocation.Proceed();
        if (invocation.ReturnValue is Task returnValueTask)
        {
            returnValueTask.GetAwaiter().GetResult();
        }

        if (invocation.ReturnValue is Task task && task.Exception != null)
        {
            throw task.Exception;
        }
        
        Console.WriteLine(
            invocation.Method.DeclaringType!.Name + "." +
            invocation.Method.Name + " -> " +
            _stopwatch.Elapsed.TotalSeconds +
            " Seconds");
        _stopwatch.Reset();
    }
}
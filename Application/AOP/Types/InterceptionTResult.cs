using Application.AOP.Bases;
using Castle.DynamicProxy;

namespace Application.AOP.Types;

public class InterceptionTResult<T> : BaseInterception where T : new()
{
    public override void Intercept(IInvocation invocation)
    {
        invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
    }

    private async Task<T> InternalInterceptAsynchronous(IInvocation invocation)
    {
        OnBefore(invocation);
        var result = new T();
        try
        {
            invocation.Proceed();
            var task = (Task<T>)invocation.ReturnValue;
            result = await task;
            OnSuccess(invocation, result);
        }
        catch (Exception e)
        {
            OnException(invocation, e);
        }

        OnAfter(invocation);

        return result;
    }

    protected virtual void OnBefore(IInvocation invocation)
    {
    }

    protected virtual void OnSuccess(IInvocation invocation, T result)
    {
    }

    protected virtual void OnException(IInvocation invocation, Exception exception)
    {
    }

    protected virtual void OnAfter(IInvocation invocation)
    {
    }
}
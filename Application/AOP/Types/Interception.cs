using Application.AOP.Bases;
using Castle.DynamicProxy;

namespace Application.AOP.Types;

public class Interception : BaseInterception
{
    public override void Intercept(IInvocation invocation)
    {
        var isSuccess = true;

        OnBefore(invocation);

        try
        {
            invocation.Proceed();
            if (invocation.ReturnValue is Task returnValueTask)
            {
                returnValueTask.GetAwaiter().GetResult();
            }

            if (invocation.ReturnValue is Task task && task.Exception != null)
            {
                throw task.Exception;
            }
        }
        catch (Exception e)
        {
            isSuccess = false;
            OnException(invocation, e);
        }
        finally
        {
            if (isSuccess)
                OnSuccess(invocation);
        }

        OnAfter(invocation);
    }

    protected virtual void OnBefore(IInvocation invocation)
    {
    }

    protected virtual void OnException(IInvocation invocation, Exception exception)
    {
    }

    protected virtual void OnSuccess(IInvocation invocation)
    {
    }

    protected virtual void OnAfter(IInvocation invocation)
    {
    }
}
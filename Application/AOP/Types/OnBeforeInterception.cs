using Application.AOP.Bases;
using Castle.DynamicProxy;

namespace Application.AOP.Types;

public class OnBeforeInterception : BaseInterception
{
    public override void Intercept(IInvocation invocation)
    {
        OnBefore(invocation);
        invocation.Proceed();
    }

    protected virtual void OnBefore(IInvocation invocation)
    {
    }
}
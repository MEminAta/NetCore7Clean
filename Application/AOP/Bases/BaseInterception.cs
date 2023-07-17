using Castle.DynamicProxy;

namespace Application.AOP.Bases;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public abstract class BaseInterception : Attribute, IInterceptor
{
    public int Priority { get; set; }
    public virtual void Intercept(IInvocation invocation)
    {
    }
}
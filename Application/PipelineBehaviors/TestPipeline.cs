using MediatR;

namespace Application.PipelineBehaviors;

public class TestPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : new()
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Console.WriteLine("Selam");
        return Task.FromResult(new TResponse());
    }
}
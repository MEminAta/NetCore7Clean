using MediatR;

namespace Application.PipelineBehaviors;

public class TestPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        Console.WriteLine("TestPipeLine 1");

        var response = await next();

        Console.WriteLine("TestPipeLine 2");
        return response;
    }
}
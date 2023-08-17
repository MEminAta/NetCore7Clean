using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.PipelineBehaviors;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }


        ValidationContext<object> context = new(request);
        List<ValidationFailure> failures = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .Distinct()
            .ToList();
        if (failures.Count != 0) throw new ValidationException(failures);
        Console.WriteLine("ValidationPipeLine 1");
        return await next();
    }
}
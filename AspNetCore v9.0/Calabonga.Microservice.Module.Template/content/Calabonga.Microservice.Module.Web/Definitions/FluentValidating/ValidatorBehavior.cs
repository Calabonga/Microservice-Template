using FluentValidation;
using Mediator;

namespace Calabonga.Microservice.Module.Web.Definitions.FluentValidating;

/// <summary>
/// Base validator for requests
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
    {
        var failures = _validators
            .Select(x => x.Validate(new ValidationContext<TRequest>(message)))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .ToList();

        return failures.Any()
            ? throw new ValidationException(failures)
            : next(message, cancellationToken);
    }
}

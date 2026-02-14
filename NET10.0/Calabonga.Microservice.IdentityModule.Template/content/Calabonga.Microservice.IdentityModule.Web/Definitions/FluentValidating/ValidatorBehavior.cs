using FluentValidation;
using Mediator;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.FluentValidating;

/// <summary>
/// Base validator for requests
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public sealed class ValidatorBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Pipeline handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary
    /// </summary>
    /// <param name="message">Incoming request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
    /// <returns>Awaitable task returning the <typeparamref name="TResponse" /></returns>
    public ValueTask<TResponse> Handle(TRequest message, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
    {
        var failures = validators
            .Select(x => x.Validate(new ValidationContext<TRequest>(message)))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .ToList();

        return failures.Any()
            ? throw new ValidationException(failures)
            : next(message, cancellationToken);
    }

}

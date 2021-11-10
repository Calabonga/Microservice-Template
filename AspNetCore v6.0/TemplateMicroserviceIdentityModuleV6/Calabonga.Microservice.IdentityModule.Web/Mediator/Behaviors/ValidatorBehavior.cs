using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using FluentValidation;
using MediatR;

namespace $safeprojectname$.Mediator.Behaviors;

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

    /// <summary>
    /// Pipeline handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary
    /// </summary>
    /// <param name="request">Incoming request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
    /// <returns>Awaitable task returning the <typeparamref name="TResponse" /></returns>
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var failures = _validators
            .Select(x => x.Validate(new ValidationContext<TRequest>(request)))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .ToList();

        if (!failures.Any())
        {
            return next();
        }

        var type = typeof(TResponse);
        if (!type.IsSubclassOf(typeof(OperationResult)))
        {
            var exception = new ValidationException(failures);
            throw exception;
        }

        var result = Activator.CreateInstance(type);
        (result as OperationResult).AddError(new MicroserviceEntityValidationException());
        foreach (var failure in failures)
        {
            (result as OperationResult)?.AppendLog($"{failure.PropertyName}: {failure.ErrorMessage}");
        }
        return Task.FromResult((TResponse) result);
    }
}
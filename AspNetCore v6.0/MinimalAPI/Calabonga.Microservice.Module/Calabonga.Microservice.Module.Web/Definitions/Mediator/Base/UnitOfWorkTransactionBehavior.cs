using Calabonga.UnitOfWork;
using MediatR;

namespace Calabonga.Microservice.Module.Web.Definitions.Mediator.Base
{
    /// <summary>
    /// Transaction Behavior
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehavior(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var response = await next();
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
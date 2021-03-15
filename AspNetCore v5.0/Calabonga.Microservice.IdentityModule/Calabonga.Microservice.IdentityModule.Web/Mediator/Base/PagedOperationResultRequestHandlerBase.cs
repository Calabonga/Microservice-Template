using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Base
{
    /// <summary>
    /// Mediator base request with OperationResult
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    public abstract class PagedOperationResultRequestHandlerBase<TRequest, TResponse> : RequestHandlerBase<TRequest, OperationResult<IPagedList<TResponse>>>
        where TResponse : IViewModel
        where TRequest : IRequest<OperationResult<IPagedList<TResponse>>>
    {
        /// <summary>
        /// Process operation result
        /// </summary>
        /// <param name="operationResult"></param>
        /// <returns></returns>
        protected virtual OperationResult<TResponse> OperationResultBeforeReturn(OperationResult<TResponse> operationResult) => operationResult;

        /// <summary>
        /// Process operation result
        /// </summary>
        /// <param name="operationResult"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected virtual OperationResult<IPagedList<TResponse>> ProcessOperationResult(OperationResult<IPagedList<TResponse>> operationResult, IPagedList<TResponse> response) => null;
    }
}
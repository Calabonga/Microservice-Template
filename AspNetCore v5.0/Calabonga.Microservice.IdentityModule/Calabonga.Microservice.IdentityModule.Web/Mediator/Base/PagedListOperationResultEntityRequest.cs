using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Base
{
    /// <summary>
    /// Mediator base request with OperationResult
    /// </summary>
    /// <typeparam name="TQueryParams"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class PagedListOperationResultEntityRequest<TResponse, TQueryParams>
        : RequestBase<OperationResult<IPagedList<TResponse>>>
        where TQueryParams : PagedListQueryParams
        where TResponse : ViewModelBase
    {
        protected PagedListOperationResultEntityRequest(TQueryParams queryParams) => QueryParams = queryParams;

        public TQueryParams QueryParams { get; }
    }
}

using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.Microservice.IdentityModule.Web.Mediator.Base;
using Calabonga.Microservices.Core.QueryParams;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Queries
{
    /// <summary>
    /// Get Paged Query base implementation
    /// </summary>
    public abstract class GetPagedQuery<TResponse, TQueryParams> :
        PagedListOperationResultEntityRequest<TResponse, TQueryParams>
        where TQueryParams : PagedListQueryParams
        where TResponse : ViewModelBase
    {
        protected GetPagedQuery(TQueryParams queryParams)
            : base(queryParams)
        {
            
        }
    }
}
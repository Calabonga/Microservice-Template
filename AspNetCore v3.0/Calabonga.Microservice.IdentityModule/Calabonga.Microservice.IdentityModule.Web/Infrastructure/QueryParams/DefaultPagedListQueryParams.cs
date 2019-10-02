using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.QueryParams;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.QueryParams
{
    /// <summary>
    /// Default paged list query with default pages size
    /// </summary>
    public class DefaultPagedListQueryParams : PagedListQueryParams
    {
        public DefaultPagedListQueryParams()
        {
            PageSize = 10;
        }
    }
}

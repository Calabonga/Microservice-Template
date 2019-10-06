namespace Calabonga.Microservices.Core.QueryParams
{
    /// <summary>
    /// Query Params with result as PagedList 
    /// </summary>
    public interface IPagedListQueryParams : IQueryParams {

        /// <summary>
        /// Page index
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        int PageSize { get; set; }
    }
}
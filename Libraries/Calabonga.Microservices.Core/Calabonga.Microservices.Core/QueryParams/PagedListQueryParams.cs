namespace Calabonga.Microservices.Core.QueryParams
{
    /// <summary>
    /// Operation result for PagedList requesting
    /// </summary>
    public class PagedListQueryParams : QueryParamsBase, IPagedListQueryParams
    {
        /// <summary>
        /// Page index
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Sorting direction
        /// </summary>
        public QueryParamsSortDirection SortDirection { get; set; }
    }
}

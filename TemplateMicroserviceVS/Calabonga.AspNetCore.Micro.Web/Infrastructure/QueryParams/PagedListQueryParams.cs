using Calabonga.OperationResultsCore;

namespace $safeprojectname$.Infrastructure.QueryParams
{
    /// <summary>
    /// Default paged list query params for pagination
    /// </summary>
    public class PagedListQueryParams: PagedListQueryParamsBase
    {

        /// <inheritdoc />
        public PagedListQueryParams()
        {
            PageIndex = 0;
            SortDirection = SortDirection.Ascending;
        }

        /// <summary>
        /// Sorting direction Ascending or Descending
        /// </summary>
        public SortDirection SortDirection { get; set; }
    }
}

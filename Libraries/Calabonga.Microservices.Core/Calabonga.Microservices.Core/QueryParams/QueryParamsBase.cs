namespace Calabonga.Microservices.Core.QueryParams
{
    /// <summary>
    /// Query Parameter base implementation of IQueryParams
    /// </summary>
    public class QueryParamsBase : IQueryParams
    {

        /// <summary>
        /// Search term for current request
        /// </summary>
        public string Search { get; set; }
    }
}
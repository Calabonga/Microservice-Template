namespace Calabonga.Microservices.Core.QueryParams
{
    /// <summary>
    /// Query Parameter interface
    /// </summary>
    public interface IQueryParams
    {

        /// <summary>
        /// Search term for current request
        /// </summary>
        string Search { get; set; }
    }
}
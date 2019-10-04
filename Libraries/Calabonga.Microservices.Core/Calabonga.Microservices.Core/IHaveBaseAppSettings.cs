namespace Calabonga.Microservices.Core
{
    /// <summary>
    /// Base settings for Unit of Work
    /// </summary>
    public interface IHaveBaseAppSettings
    {
        /// <summary>
        /// Default page size
        /// </summary>
        int PageSize { get; set; }
    }
}
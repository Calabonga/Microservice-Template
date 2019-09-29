namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Settings.Base
{
    /// <summary>
    /// Base service behavior
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Indicates that the processing available works is enabled
        /// </summary>
        public bool IsActive { get; set; }
    }
}
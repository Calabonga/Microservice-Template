using Calabonga.Microservice.IdentityModule.Domain.Base;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints.EventItemsEndpoints.ViewModels
{
    public class EventItemCreateViewModel : IViewModel
    {
        /// <summary>
        /// EventItem Created At
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Service name or provider
        /// </summary>
        public string Logger { get; set; } = null!;

        /// <summary>
        /// EventItem level for logging. See <see cref="LogLevel"/>
        /// </summary>
        public string Level { get; set; } = null!;

        /// <summary>
        /// EventItem Message
        /// </summary>
        public string Message { get; set; } = null!;

        /// <summary>
        /// Thread identifier
        /// </summary>
        public string? ThreadId { get; set; }

        /// <summary>
        /// Exception message
        /// </summary>
        public string? ExceptionMessage { get; set; }
    }
}
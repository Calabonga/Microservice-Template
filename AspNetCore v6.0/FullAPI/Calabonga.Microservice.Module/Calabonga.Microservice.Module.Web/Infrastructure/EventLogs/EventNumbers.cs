using Microsoft.Extensions.Logging;

namespace Calabonga.Microservice.Module.Web.Infrastructure.EventLogs
{
    /// <summary>
    /// The number identifiers for events in the microservices
    /// </summary>
    static class EventNumbers
    {
        internal static readonly EventId PostItemId = new EventId(9002, nameof(PostItemId));
    }
}

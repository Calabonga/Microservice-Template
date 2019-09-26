using System;

namespace Calabonga.Microservice.Module.Web.Infrastructure.Settings
{
    /// <summary>
    /// Represents configuration file with current application settings
    /// </summary>
    public class CurrentAppSettings
    {
        /// <summary>
        /// Default page size
        /// </summary>
        public int DefaultPageSize { get; set; }

        /// <summary>
        /// Domain name link (example: for Notification Builder)
        /// </summary>
        public string DomainName { get; set; } = "PricePoint";

        /// <summary>
        /// Default url to PricePoint site
        /// </summary>
        public string DomainUrl { get; set; } = "http://localhost:22331";

        /// <summary>
        /// For some operations currency identifier is required
        /// </summary>
        public Guid DefaultCurrencyId { get; set; }

        /// <summary>
        /// Time machine emulation
        /// </summary>
        public DateTime CurrentDateTime { get; set; }

        /// <summary>
        /// SMTP email server settings
        /// </summary>
        public MailServerSettings MailServerSettings { get; set; } = new MailServerSettings();
    }
}

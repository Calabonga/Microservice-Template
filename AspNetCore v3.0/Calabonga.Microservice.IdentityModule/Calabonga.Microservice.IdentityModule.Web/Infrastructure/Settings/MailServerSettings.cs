using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Settings.Base;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Settings
{
    /// <summary>
    /// Mail service settings for <see cref="CurrentAppSettings"/>
    /// </summary>
    public class MailServerSettings: ServiceBase
    {
        /// <summary>
        /// SMTP server name
        /// </summary>
        public string SmtpServer { get; set; }
        
        /// <summary>
        /// Smtp port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Sender name
        /// </summary>
        public string RobotName { get; set; }

        /// <summary>
        /// Sender email (no reply)
        /// </summary>
        public string RobotEmail { get; set; }

        /// <summary>
        /// Account name for SmtpServer authentication
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password for SmtpServer authentication
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// SSL protocol for MailKit
        /// </summary>
        public string SslProtocol { get; set; }

        /// <summary>
        /// Enables/Disable SmtpClient logger to console
        /// </summary>
        public bool SmtpClientLoggerEnabled { get; set; }
    }
}
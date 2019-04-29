namespace Calabonga.AspNetCore.MicroModule.Core
{
    /// <summary>
    /// Mail message interface
    /// </summary>
    public interface IEmailMessage
    {
        /// <summary>
        /// Mail to
        /// </summary>
        string MailTo { get; set; }

        /// <summary>
        /// Mail from
        /// </summary>
        string MailFrom { get; set; }

        /// <summary>
        /// Subject or title of the message 
        /// </summary>
        string Subject { get; set; }

        /// <summary>
        /// Message content
        /// </summary>
        string Body { get; set; }
    }
}
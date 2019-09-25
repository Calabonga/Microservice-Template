namespace Calabonga.AspNetCore.MicroModule.Core
{
    /// <summary>
    /// Mail Message to send
    /// </summary>
    public class EmailMessage : IEmailMessage
    {

        /// <inheritdoc />
        public EmailMessage()
        {
            Result = new SendEmailResult();
        }

        /// <inheritdoc />
        public string MailTo { get; set; }

        /// <inheritdoc />
        public string Subject { get; set; }

        /// <inheritdoc />
        public string Body { get; set; }

        /// <summary>
        /// Sent result info
        /// </summary>
        public SendEmailResult Result { get; }

        /// <summary>
        /// Use HTML in the Body
        /// </summary>
        public bool IsHtml { get; set; }

        /// <inheritdoc />
        public string MailFrom { get; set; }
    }
}

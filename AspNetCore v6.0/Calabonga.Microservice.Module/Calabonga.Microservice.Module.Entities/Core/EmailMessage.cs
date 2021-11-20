namespace Calabonga.Microservice.Module.Entities.Core
{
    /// <summary>
    /// Mail Message to send
    /// </summary>
    public class EmailMessage : IEmailMessage
    {

        public EmailMessage()
        {
            Result = new SendEmailResult();
        }

        public string MailTo { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        /// <summary>
        /// Sent result info
        /// </summary>
        public SendEmailResult Result { get; }

        /// <summary>
        /// Use HTML in the Body
        /// </summary>
        public bool IsHtml { get; set; }

        public string MailFrom { get; set; }
    }
}

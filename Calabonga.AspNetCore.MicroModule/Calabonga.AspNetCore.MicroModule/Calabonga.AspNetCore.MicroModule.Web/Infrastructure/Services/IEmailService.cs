using System.Collections.Generic;
using System.Threading.Tasks;
using Calabonga.AspNetCore.MicroModule.Core;
using Microsoft.AspNetCore.Http;

namespace Calabonga.AspNetCore.MicroModule.Web.Infrastructure.Services
{
    /// <summary>
    /// Email Service messaging
    /// </summary>
    public interface IEmailService
    {

        /// <summary>
        /// Send email with attachments
        /// </summary>
        /// <param name="mailto"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailBody"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        Task<EmailMessage> SendMailAsync(string mailto, string mailSubject, string mailBody, ICollection<IFormFile> files);

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="mailto"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailBody"></param>
        /// <returns></returns>
        Task<EmailMessage> SendMailAsync(string mailto, string mailSubject, string mailBody);

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<EmailMessage> SendMailAsync(IEmailMessage message);
    }
}
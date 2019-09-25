using System.Collections.Generic;
using System.Threading.Tasks;
using $ext_safeprojectname$.Core;
using Microsoft.AspNetCore.Http;

namespace $safeprojectname$.Infrastructure.Services
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
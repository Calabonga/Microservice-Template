using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calabonga.AspNetCore.Micro.Core;
using Calabonga.AspNetCore.Micro.Core.Exceptions;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Settings;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Calabonga.AspNetCore.Micro.Web.Infrastructure.Services
{
    /// <summary>
    /// Email service just send the mail. Nothing else!
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly CurrentAppSettings _appSettings;

        /// <inheritdoc />
        public EmailService(IOptions<CurrentAppSettings> options, ILogger<EmailService> logger)
        {
            _logger = logger;
            _appSettings = options.Value;
        }

        /// <summary>
        /// Send email with attachments
        /// </summary>
        /// <param name="mailto"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailBody"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public Task<EmailMessage> SendMailAsync(string mailto, string mailSubject, string mailBody, ICollection<IFormFile> files)
        {
            return SendMailFromServerAsync(mailto, mailSubject, mailBody, files, true);
        }

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="mailto"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailBody"></param>
        /// <returns></returns>
        public Task<EmailMessage> SendMailAsync(string mailto, string mailSubject, string mailBody)
        {
            return SendMailFromServerAsync(mailto, mailSubject, mailBody, null, true);
        }

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<EmailMessage> SendMailAsync(IEmailMessage message)
        {
            return SendMailFromServerAsync(message.MailTo, message.Subject, message.Body, null, true);
        }

        private async Task<EmailMessage> SendMailFromServerAsync(string mailto, string mailSubject, string mailBody, ICollection<IFormFile> files, bool isHtml)
        {
            var message = new EmailMessage
            {
                MailTo = mailto,
                MailFrom = _appSettings.MailServerSettings.RobotEmail,
                Subject = mailSubject,
                Body = mailBody,
                IsHtml = isHtml
            };

            if (_appSettings.MailServerSettings.IsActive)
            {
                message.Result.IsInProcess = true;
                try
                {
                    var emailMessage = new MimeMessage();
                    emailMessage.From.Add(new MailboxAddress(_appSettings.MailServerSettings.RobotName, message.MailFrom));
                    emailMessage.To.Add(new MailboxAddress(message.MailTo));
                    emailMessage.Subject = message.Subject;
                    emailMessage.Body = new TextPart("html") { Text = message.Body };

                    using (var client = _appSettings.MailServerSettings.SmtpClientLoggerEnabled ?
                        new SmtpClient(new ProtocolLogger(Console.OpenStandardOutput())): new SmtpClient())
                    {
                        client.LocalDomain = "localhost";
                        if (string.IsNullOrEmpty(_appSettings.MailServerSettings.SslProtocol))
                        {
                            var exception = new MicroserviceArgumentNullException(AppData.Exceptions.EmailServiceUnavailable);
                            _logger.LogError(exception, AppData.Exceptions.EmailServiceUnavailable);
                            message.Result.IsInProcess = false;
                            message.Result.Exception = exception;
                            message.Result.IsSent = false;
                            return await Task.FromResult(message);
                        }
                        var sslProtocol = (SecureSocketOptions)Enum.Parse(typeof(SecureSocketOptions), _appSettings.MailServerSettings.SslProtocol);
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        await client.ConnectAsync(_appSettings.MailServerSettings.SmtpServer, _appSettings.MailServerSettings.Port, sslProtocol).ConfigureAwait(false);
                        client.Authenticate(_appSettings.MailServerSettings.UserName, _appSettings.MailServerSettings.Password);
                        await client.SendAsync(emailMessage).ConfigureAwait(false);
                        await client.DisconnectAsync(true).ConfigureAwait(false);
                    }
                    message.Result.IsInProcess = false;
                    message.Result.IsSent = true;
                    _logger.LogInformation($"Message for [{emailMessage.To}] through [{_appSettings.MailServerSettings.SmtpServer}] successfully sent");

                    return await Task.FromResult(message);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, AppData.Exceptions.EmailServiceUnavailable);
                    message.Result.IsInProcess = false;
                    message.Result.Exception = exception;
                    message.Result.IsSent = false;
                    return await Task.FromResult(message);
                }
            }
            message.Result.IsInProcess = false;
            message.Result.IsSent = true;
            _logger.LogInformation("In DEBUG mode email service disabled");
            return await Task.FromResult(message);
        }
    }
}
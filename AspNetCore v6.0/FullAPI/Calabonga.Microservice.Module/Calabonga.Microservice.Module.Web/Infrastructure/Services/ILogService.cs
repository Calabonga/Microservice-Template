namespace Calabonga.Microservice.Module.Web.Infrastructure.Services
{
    /// <summary>
    /// Interface for Business logic logger can save messages to database
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Log information message
        /// </summary>
        /// <param name="message"></param>
        void LogInformation(string message);
    }
}

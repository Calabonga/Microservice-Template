using System;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.ViewModels.LogViewModels
{
    /// <summary>
    /// Log ViewModel
    /// </summary>
    public class LogViewModel: ViewModelBase
    {
        /// <summary>
        /// Created at
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Logger name
        /// </summary>
        public string Logger { get; set; }

        /// <summary>
        /// Level
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// Message text
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Thread ID
        /// </summary>
        public string ThreadId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExceptionMessage { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Microsoft.Extensions.Logging;

namespace Calabonga.Microservice.Module.Web.ViewModels.LogViewModels
{
    /// <summary>
    /// Data Transfer Object for Log entity
    /// </summary>
    public class LogCreateViewModel : IViewModel
    {
        /// <summary>
        /// Log Created At
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Service name or provider
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Logger { get; set; }

        /// <summary>
        /// Log level for logging. See <see cref="LogLevel"/>
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Level { get; set; }

        /// <summary>
        /// Log Message
        /// </summary>
        [Required]
        [StringLength(4000)]
        public string Message { get; set; }

        /// <summary>
        /// Thread identifier
        /// </summary>
        public string ThreadId { get; set; }

        /// <summary>
        /// Exception message
        /// </summary>
        public string ExceptionMessage { get; set; }
    }
}
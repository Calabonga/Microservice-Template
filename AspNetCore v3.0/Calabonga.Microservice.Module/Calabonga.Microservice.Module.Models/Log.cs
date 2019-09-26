using System;
using Calabonga.Microservice.Module.Models.Base;

namespace Calabonga.Microservice.Module.Models
{
    /// <summary>
    /// Logs
    /// </summary>
    public class Log : Identity
    {
        public DateTime CreatedAt { get; set; }

        public string Logger { get; set; }

        public string Level { get; set; }

        public string Message { get; set; }

        public string ThreadId { get; set; }

        public string ExceptionMessage { get; set; }
    }
}

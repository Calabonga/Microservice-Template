using $safeprojectname$.Base;

namespace $safeprojectname$
{
    /// <summary>
    /// Log entity for demo purposes only
    /// </summary>
    public class Log : Identity
    {
        public DateTime CreatedAt { get; set; }

        public string Logger { get; set; } = null!;

        public string Level { get; set; } = null!;

        public string Message { get; set; } = null!;

        public string? ThreadId { get; set; }

        public string? ExceptionMessage { get; set; }
    }
}
using $ext_projectname$.Domain.Base;

namespace $safeprojectname$.Endpoints.LogsEndpoints.ViewModels;

public class LogCreateViewModel : IViewModel
{
    /// <summary>
    /// Log Created At
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Service name or provider
    /// </summary>
    public string Logger { get; set; } = null!;

    /// <summary>
    /// Log level for logging. See <see cref="LogLevel"/>
    /// </summary>
    public string Level { get; set; } = null!;

    /// <summary>
    /// Log Message
    /// </summary>
    public string Message { get; set; } = null!;

    /// <summary>
    /// Thread identifier
    /// </summary>
    public string? ThreadId { get; set; }

    /// <summary>
    /// Exception message
    /// </summary>
    public string? ExceptionMessage { get; set; }
}
using Calabonga.EntityFrameworkCore.Entities.Base;
using System;

namespace Calabonga.Microservice.Module.Web.Features.Logs;

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
    public string Logger { get; set; } = null!;

    /// <summary>
    /// Level
    /// </summary>
    public string Level { get; set; } = null!;

    /// <summary>
    /// Message text
    /// </summary>
    public string Message { get; set; } = null!;

    /// <summary>
    /// Thread ID
    /// </summary>
    public string? ThreadId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? ExceptionMessage { get; set; }
}
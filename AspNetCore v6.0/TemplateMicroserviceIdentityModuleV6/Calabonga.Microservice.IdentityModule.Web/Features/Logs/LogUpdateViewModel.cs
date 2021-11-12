using Calabonga.EntityFrameworkCore.Entities.Base;

namespace $safeprojectname$.Features.Logs;

/// <summary>
/// Log View model for Update operations
/// </summary>
public class LogUpdateViewModel : ViewModelBase
{
    public string Logger { get; set; } = null!;

    public string Level { get; set; } = null!;

    public string Message { get; set; } = null!;
}
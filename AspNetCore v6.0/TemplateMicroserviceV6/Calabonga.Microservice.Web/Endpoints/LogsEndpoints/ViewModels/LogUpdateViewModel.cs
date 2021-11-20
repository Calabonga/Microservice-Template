using $ext_projectname$.Domain.Base;

namespace $safeprojectname$.Endpoints.LogsEndpoints.ViewModels;

public class LogUpdateViewModel : ViewModelBase
{
    public string Logger { get; set; } = null!;

    public string Level { get; set; } = null!;

    public string Message { get; set; } = null!;
}
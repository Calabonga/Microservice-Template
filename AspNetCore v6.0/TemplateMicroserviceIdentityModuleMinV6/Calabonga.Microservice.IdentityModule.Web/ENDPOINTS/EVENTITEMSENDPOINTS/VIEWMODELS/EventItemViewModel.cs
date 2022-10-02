namespace $safeprojectname$.Endpoints.EventItemsEndpoints.ViewModels;

public class EventItemViewModel
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Logger { get; set; } = null!;

    public string Level { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? ThreadId { get; set; }

    public string? ExceptionMessage { get; set; }
}
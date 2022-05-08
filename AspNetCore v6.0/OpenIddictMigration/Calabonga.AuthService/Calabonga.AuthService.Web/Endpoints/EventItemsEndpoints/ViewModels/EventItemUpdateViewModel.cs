using Calabonga.AuthService.Domain.Base;

namespace Calabonga.AuthService.Web.Endpoints.EventItemsEndpoints.ViewModels;

public class EventItemUpdateViewModel : ViewModelBase
{
    public string Logger { get; set; } = null!;

    public string Level { get; set; } = null!;

    public string Message { get; set; } = null!;
}
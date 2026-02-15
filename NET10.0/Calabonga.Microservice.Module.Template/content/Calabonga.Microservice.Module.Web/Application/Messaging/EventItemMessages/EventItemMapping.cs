using Calabonga.Microservice.Module.Domain;
using Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.ViewModels;

namespace Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages;

/// <summary>
/// Replacement for Automapper
/// </summary>
public static class EventItemMapping
{
    /// <summary>
    /// Creates a ViewModel
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static EventItemViewModel? MapToViewModel(this EventItem? source)
    {
        if (source is null)
        {
            return null;
        }

        return new EventItemViewModel
        {
            CreatedAt = source.CreatedAt,
            ExceptionMessage = source.ExceptionMessage,
            Id = source.Id,
            Level = source.Level,
            Logger = source.Logger,
            Message = source.Message,
            ThreadId = source.ThreadId,
        };
    }

    /// <summary>
    /// Creates an EventItem from ViewModel
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static EventItem? MapToEventItem(this EventItemCreateViewModel? source)
    {
        if (source is null)
        {
            return null;
        }

        return new EventItem
        {
            CreatedAt = source.CreatedAt,
            Level = source.Level,
            Logger = source.Logger,
            Message = source.Message,
            ThreadId = source.ThreadId
        };
    }

    /// <summary>
    /// Creates an EventItem from ViewModel 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="updateViewModel"></param>
    /// <returns></returns>
    public static void MapUpdatesFrom(this EventItem? source, EventItemUpdateViewModel? updateViewModel)
    {
        if (updateViewModel is null || source is null)
        {
            return;
        }

        source.Level = updateViewModel.Level;
        source.Logger = updateViewModel.Logger;
        source.Message = updateViewModel.Message;
        source.Id = updateViewModel.Id;
    }
}

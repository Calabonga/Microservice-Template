using AutoMapper;
using Calabonga.Microservice.IdentityModule.Domain;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.Microservice.IdentityModule.Web.Definitions.Mapping;
using Calabonga.PagedListCore;

namespace Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages;

/// <summary>
/// Mapper Configuration for entity EventItem
/// </summary>
public class EventItemMapperConfiguration : Profile
{
    /// <inheritdoc />
    public EventItemMapperConfiguration()
    {
        CreateMap<EventItemCreateViewModel, EventItem>()
            .ForMember(x => x.Id, o => o.Ignore());

        CreateMap<EventItem, EventItemViewModel>();

        CreateMap<EventItem, EventItemUpdateViewModel>();

        CreateMap<EventItemUpdateViewModel, EventItem>()
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.ThreadId, o => o.Ignore())
            .ForMember(x => x.ExceptionMessage, o => o.Ignore());

        CreateMap<IPagedList<EventItem>, IPagedList<EventItemViewModel>>()
            .ConvertUsing<PagedListConverter<EventItem, EventItemViewModel>>();
    }
}
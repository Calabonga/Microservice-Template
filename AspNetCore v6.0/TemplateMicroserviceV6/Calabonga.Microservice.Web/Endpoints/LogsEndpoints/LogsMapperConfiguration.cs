using AutoMapper;
using $ext_projectname$.Domain;
using $safeprojectname$.Definitions.Mapping;
using $safeprojectname$.Endpoints.LogsEndpoints.ViewModels;
using Calabonga.UnitOfWork;

namespace $safeprojectname$.Endpoints.LogsEndpoints;

/// <summary>
/// Mapper Configuration for entity Log
/// </summary>
public class LogsMapperConfiguration : Profile
{
    /// <inheritdoc />
    public LogsMapperConfiguration()
    {
        CreateMap<LogCreateViewModel, Log>()
            .ForMember(x => x.Id, o => o.Ignore());

        CreateMap<Log, LogViewModel>();

        CreateMap<Log, LogUpdateViewModel>();

        CreateMap<LogUpdateViewModel, Log>()
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.ThreadId, o => o.Ignore())
            .ForMember(x => x.ExceptionMessage, o => o.Ignore());

        CreateMap<IPagedList<Log>, IPagedList<LogViewModel>>()
            .ConvertUsing<PagedListConverter<Log, LogViewModel>>();
    }
}
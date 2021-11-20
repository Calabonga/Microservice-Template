using Calabonga.Microservice.Module.Entities;
using Calabonga.Microservice.Module.Web.Features.Logs;
using Calabonga.Microservice.Module.Web.Infrastructure.Mappers.Base;
using Calabonga.UnitOfWork;

namespace Calabonga.Microservice.Module.Web.Infrastructure.Mappers
{
    /// <summary>
    /// Mapper Configuration for entity Log
    /// </summary>
    public class LogMapperConfiguration: MapperConfigurationBase
    {
        public LogMapperConfiguration()
        {
            CreateMap<LogCreateViewModel, Log>()
                .ForMember(x=>x.Id, o=>o.Ignore());

            CreateMap<Log, LogViewModel>();

            CreateMap<IPagedList<Log>, IPagedList<LogViewModel>>()
                .ConvertUsing<PagedListConverter<Log, LogViewModel>>();
        }
    }
}

using Calabonga.EntityFrameworkCore.UOW;
using Calabonga.Microservice.Module.Models;
using Calabonga.Microservice.Module.Web.Infrastructure.Mappers.Base;
using Calabonga.Microservice.Module.Web.Infrastructure.ViewModels.LogViewModels;

namespace Calabonga.Microservice.Module.Web.Infrastructure.Mappers
{
    /// <summary>
    /// Mapper Configuration for entity Log
    /// </summary>
    public class LogMapperConfiguration: MapperConfigurationBase
    {
        /// <inheritdoc />
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

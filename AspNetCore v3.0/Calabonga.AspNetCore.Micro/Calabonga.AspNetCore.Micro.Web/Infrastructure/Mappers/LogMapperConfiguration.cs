using Calabonga.AspNetCore.Micro.Models;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Mappers.Base;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.ViewModels.LogViewModels;
using Calabonga.EntityFrameworkCore.UOW;

namespace Calabonga.AspNetCore.Micro.Web.Infrastructure.Mappers
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

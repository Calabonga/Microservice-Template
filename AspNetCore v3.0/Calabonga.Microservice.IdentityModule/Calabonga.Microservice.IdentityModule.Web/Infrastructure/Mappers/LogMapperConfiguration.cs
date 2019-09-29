using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.Microservice.IdentityModule.Models;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Mappers.Base;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.ViewModels.LogViewModels;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Mappers
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

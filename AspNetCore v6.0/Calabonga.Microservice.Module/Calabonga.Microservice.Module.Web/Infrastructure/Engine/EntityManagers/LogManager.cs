using System.Security.Principal;
using AutoMapper;
using Calabonga.Microservice.Module.Entities;
using Calabonga.Microservice.Module.Web.Features.Logs;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;

namespace Calabonga.Microservice.Module.Web.Infrastructure.Engine.EntityManagers
{
    /// <summary>
    /// Entity manager for <see cref="Log"/>
    /// </summary>
    public class LogManager: EntityManager<LogViewModel, Log, LogCreateViewModel, LogUpdateViewModel>
    {
        public LogManager(IMapper mapper, IViewModelFactory<LogCreateViewModel, LogUpdateViewModel> viewModelFactory, IEntityValidator<Log> validator)
            : base(mapper, viewModelFactory, validator)
        {
        }

        protected override IIdentity? GetIdentity()
        {
            return null;
        }
    }
}

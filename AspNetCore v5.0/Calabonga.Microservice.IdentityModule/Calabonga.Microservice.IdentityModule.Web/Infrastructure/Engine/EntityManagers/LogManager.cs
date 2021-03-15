using System.Security.Principal;
using AutoMapper;

using Calabonga.Microservice.IdentityModule.Entities;
using Calabonga.Microservice.IdentityModule.Web.ViewModels.LogViewModels;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Engine.EntityManagers
{
    /// <summary>
    /// Entity manager for <see cref="Log"/>
    /// </summary>
    public class LogManager : EntityManager<LogViewModel, Log, LogCreateViewModel, LogUpdateViewModel>
    {
        /// <inheritdoc />
        public LogManager(IMapper mapper, IViewModelFactory<LogCreateViewModel, LogUpdateViewModel> viewModelFactory, IEntityValidator<Log> validator)
            : base(mapper, viewModelFactory, validator)
        {
        }

        protected override IIdentity? GetIdentity() => null;
    }
}

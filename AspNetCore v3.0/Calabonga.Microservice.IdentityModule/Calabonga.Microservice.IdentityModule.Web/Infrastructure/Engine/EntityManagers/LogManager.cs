using AutoMapper;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Factories;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;
using Calabonga.Microservice.IdentityModule.Entities;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.ViewModels.LogViewModels;
using Calabonga.Microservices.Core.Validators;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Engine.EntityManagers
{
    /// <summary>
    /// Entity manager for <see cref="Log"/>
    /// </summary>
    public class LogManager: EntityManager<LogViewModel, Log, LogCreateViewModel, LogUpdateViewModel>
    {
        /// <inheritdoc />
        public LogManager(IMapper mapper, IViewModelFactory<LogCreateViewModel, LogUpdateViewModel> viewModelFactory, IEntityValidator<Log> validator)
            : base(mapper, viewModelFactory, validator)
        {
        }
    }
}

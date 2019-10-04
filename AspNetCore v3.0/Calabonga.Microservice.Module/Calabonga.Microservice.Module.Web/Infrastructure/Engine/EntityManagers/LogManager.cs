using AutoMapper;
using Calabonga.EntityFrameworkCore.UOW.Framework.Factories;
using Calabonga.EntityFrameworkCore.UOW.Framework.Managers;
using Calabonga.Microservice.Module.Entities;
using Calabonga.Microservice.Module.Web.Infrastructure.ViewModels.LogViewModels;
using Calabonga.Microservices.Core.Validators;

namespace Calabonga.Microservice.Module.Web.Infrastructure.Engine.EntityManagers
{
    /// <summary>
    /// Entity manager for <see cref="Log"/>
    /// </summary>
    public class LogManager: EntityManager<Log, LogCreateViewModel, LogUpdateViewModel>
    {
        /// <inheritdoc />
        public LogManager(IMapper mapper, IViewModelFactory<LogCreateViewModel, LogUpdateViewModel> viewModelFactory, IEntityValidator<Log> validator)
            : base(mapper, viewModelFactory, validator)
        {
        }
    }
}

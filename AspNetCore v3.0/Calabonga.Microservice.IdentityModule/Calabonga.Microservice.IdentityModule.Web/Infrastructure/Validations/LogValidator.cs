using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.Microservice.IdentityModule.Entities;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Validations.Base;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Validations
{
    /// <summary>
    /// Validator for entity Log
    /// </summary>
    public class LogValidator : EntityValidator<Log>
    {
        /// <inheritdoc />
        public LogValidator(IRepositoryFactory factory) : base(factory)
        {
        }
    }
}
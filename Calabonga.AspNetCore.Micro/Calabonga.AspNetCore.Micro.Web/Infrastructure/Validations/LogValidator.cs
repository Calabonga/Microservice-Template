using Calabonga.AspNetCore.Micro.Models;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Validations.Base;
using Calabonga.EntityFrameworkCore.UnitOfWork;

namespace Calabonga.AspNetCore.Micro.Web.Infrastructure.Validations
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
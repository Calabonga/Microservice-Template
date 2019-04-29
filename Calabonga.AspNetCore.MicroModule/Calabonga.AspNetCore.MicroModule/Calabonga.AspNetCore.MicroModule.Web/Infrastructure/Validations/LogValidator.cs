using Calabonga.AspNetCore.MicroModule.Models;
using Calabonga.AspNetCore.MicroModule.Web.Infrastructure.Validations.Base;
using Calabonga.EntityFrameworkCore.UOW;

namespace Calabonga.AspNetCore.MicroModule.Web.Infrastructure.Validations
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
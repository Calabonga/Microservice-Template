using $ext_safeprojectname$.Models;
using $safeprojectname$.Infrastructure.Validations.Base;
using Calabonga.EntityFrameworkCore.UnitOfWork;

namespace $safeprojectname$.Infrastructure.Validations
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
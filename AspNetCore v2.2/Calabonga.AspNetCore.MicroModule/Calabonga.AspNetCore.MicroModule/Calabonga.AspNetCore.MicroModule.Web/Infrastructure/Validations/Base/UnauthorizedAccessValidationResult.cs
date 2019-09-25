using Calabonga.AspNetCore.MicroModule.Core;

namespace Calabonga.AspNetCore.MicroModule.Web.Infrastructure.Validations.Base
{
    /// <summary>
    /// Unauthorized access validation result
    /// </summary>
    public class UnauthorizedPermissionValidationResult : PermissionValidationResult
    {
        /// <inheritdoc />
        public UnauthorizedPermissionValidationResult(string message = null)
        {
            AddError(string.IsNullOrWhiteSpace(message)
                ? AppData.Exceptions.UnauthorizedException
                : message);
        }
    }
}
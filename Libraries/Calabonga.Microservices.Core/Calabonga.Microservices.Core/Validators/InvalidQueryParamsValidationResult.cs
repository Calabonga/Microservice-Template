namespace Calabonga.Microservices.Core.Validators
{
    /// <summary>
    /// Unauthorized access validation result
    /// </summary>
    public class InvalidQueryParamsValidationResult : PermissionValidationResult
    {
        /// <inheritdoc />
        public InvalidQueryParamsValidationResult(string message = null)
        {
            AddError(string.IsNullOrWhiteSpace(message)
                ? AppContracts.Exceptions.ArgumentNullException
                : message);
        }
    }
}
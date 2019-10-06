using System.Collections.Generic;
using System.Linq;

namespace Calabonga.Microservices.Core.Validators
{
    /// <summary>
    /// Represent result for <see cref="M:ValidateUserAccessRights"/>
    /// </summary>
    public class PermissionValidationResult
    {
        private readonly List<ValidationResult> _errors;

        /// <inheritdoc />
        public PermissionValidationResult()
        {
            _errors = new List<ValidationResult>();
        }

        /// <summary>
        /// Indicated that user authorized to execute operations
        /// </summary>
        public bool IsOk
        {
            get { return !Errors.Any(); }
        }

        /// <summary>
        /// List of validation errors
        /// </summary>
        public IEnumerable<ValidationResult> Errors
        {
            get { return _errors; }
        }

        /// <summary>
        /// Append error to error list
        /// </summary>
        /// <param name="message"></param>
        public void AddError(string message)
        {
            _errors.Add(new ValidationResult(message, needToStopOtherValidations: true));
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Join(" ", Errors?.Select(x => x.Message));
        }
    }
}
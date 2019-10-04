using System.Diagnostics;

namespace Calabonga.Microservices.Core.Validators
{
    /// <summary>
    /// Entity Validation Result for validation
    /// </summary>
    [DebuggerDisplay("{Message}")]
    public class ValidationResult
    {
        /// <inheritdoc />
        public ValidationResult(string message, bool needToStopOtherValidations = false)
        {
            Message = message;
            IsNeedToStop = needToStopOtherValidations;
        }

        /// <summary>
        /// Message text
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Indicates that we should break entity validation 
        /// </summary>
        public bool IsNeedToStop { get; }
    }
}

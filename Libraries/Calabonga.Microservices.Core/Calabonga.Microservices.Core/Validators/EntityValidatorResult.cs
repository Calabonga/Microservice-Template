using System.Collections.Generic;
using System.Linq;

namespace Calabonga.Microservices.Core.Validators
{
    /// <summary>
    /// Entity
    /// </summary>
    public class ValidationContext
    {
        /// <inheritdoc />
        public ValidationContext()
        {
            Errors = new List<ValidationResult>();
        }

        /// <inheritdoc />
        public ValidationContext(List<ValidationResult> validationResults)
            : this()
        {
            Errors.AddRange(validationResults);
        }

        /// <summary>
        /// Returns error list with <see cref="ValidationResult"/>
        /// </summary>
        public List<ValidationResult> Errors { get; }

        /// <summary>
        /// Adds validation result to collection of the results
        /// </summary>
        /// <param name="result"></param>
        public void AddValidationResult(ValidationResult result)
        {
            Errors.Add(result);
        }

        /// <summary>
        /// Returns is validation completed successfully
        /// </summary>
        public bool IsValid
        {
            get
            {
                return Errors == null || !Errors.Any();
            }
        }

        /// <summary>
        /// Indicates that validation has a critical errors and need to be stopped
        /// </summary>
        public bool IsNeedToStop
        {
            get
            {
                if (Errors != null && Errors.Any())
                {
                    return Errors.Any(x => x.IsNeedToStop);
                }

                return false;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Errors == null
                ? string.Empty
                : string.Join(" ", Errors.Select(x => x.Message));
        }
    }
}
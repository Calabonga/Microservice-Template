using System.Collections.Generic;
using Calabonga.Microservices.Core.Validators;

namespace Calabonga.Microservices.Core.Extensions
{
    /// <summary>
    /// Entity Validator Extensions
    /// </summary>
    public static class EntityValidatorExtensions
    {
        /// <summary>
        /// Returns validator from validation results
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ValidationContext GetResult(this List<ValidationResult> source)
        {
            return new ValidationContext(source);
        }

    }
}
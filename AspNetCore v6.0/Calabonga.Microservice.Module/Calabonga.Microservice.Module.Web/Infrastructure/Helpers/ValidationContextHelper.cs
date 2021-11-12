using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Calabonga.Microservice.Module.Web.Infrastructure.Helpers
{
    /// <summary>
    /// ValidationContext Helper for validation operations
    /// </summary>
    public static class ValidationContextHelper
    {
        /// <summary>
        /// Returns validation results of IValidatableObject
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="results"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public static bool TryValidate(object obj, out Collection<ValidationResult> results, ValidationContext validationContext = null)
        {
            var context = validationContext ?? new ValidationContext(obj, serviceProvider: null, items: null);
            results = new Collection<ValidationResult>();
            return Validator.TryValidateObject(obj, context, results, validateAllProperties: true);
        }
    }
}

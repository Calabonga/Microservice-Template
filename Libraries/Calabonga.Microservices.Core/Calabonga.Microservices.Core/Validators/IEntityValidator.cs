using System.Collections.Generic;

namespace Calabonga.Microservices.Core.Validators
{
    /// <summary>
    /// Entity Validator interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityValidator<TEntity>
    {
        /// <summary>
        /// Common validations rules
        /// </summary>
        /// <param name="validationType"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        IEnumerable<ValidationResult> ValidateByOperationType(EntityValidationType validationType, TEntity entity);

        /// <summary>
        /// Checks entity for errors and returns. Validates entity for business logic rules on Creating operations
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IEnumerable<ValidationResult> ValidateOnInsert(TEntity entity);

        /// <summary>
        /// Checks entity for errors and returns. Validates entity for business logic rules on Editing operations
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IEnumerable<ValidationResult> ValidateOnUpdate(TEntity entity);

        /// <summary>
        /// Returns common validation results for Insert and for Update operations
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IEnumerable<ValidationResult> ValidateOnInsertOrUpdate(TEntity entity);

        /// <summary>
        /// Add custom validation result
        /// </summary>
        /// <param name="validationResult"></param>
        void AddValidationResult(ValidationResult validationResult);

        /// <summary>
        /// Indicates than the validator already has a critical errors. Validation process should be stopped.
        /// </summary>
        bool IsNeedToStop { get; }

        /// <summary>
        /// Validator result can accumulate errors for entity
        /// </summary>
        ValidationContext ValidationContext { get; set; }
    }
}

using System.Collections.Generic;
using Calabonga.EntityFrameworkCore.UnitOfWork;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Validations.Base
{
    /// <summary>
    /// Entity validator base
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EntityValidator<TEntity> : IEntityValidator<TEntity> where TEntity : class
    {
        /// <inheritdoc />
        protected EntityValidator(IRepositoryFactory factory)
        {
            Repository = factory.GetRepository<TEntity>();
            ValidationContext = new ValidationContext();
        }

        /// <summary>
        /// repository of the entity
        /// </summary>
        protected IRepository<TEntity> Repository { get; }

        /// <summary>
        /// Common validations rules
        /// </summary>
        /// <param name="validationType"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> ValidateByOperationType(EntityValidationType validationType, TEntity entity)
        {
            switch (validationType)
            {
                case EntityValidationType.None:
                    foreach (var validation in ValidationContext.Errors)
                    {
                        yield return validation;
                    }
                    break;
                case EntityValidationType.Insert:
                    foreach (var validation in ValidationContext.Errors)
                    {
                        yield return validation;
                    }

                    if (ValidationContext.IsNeedToStop)
                    {
                        break;
                    }

                    foreach (var validationResult in ValidateOnInsertOrUpdate(entity))
                    {
                        yield return validationResult;
                    }
                    
                    if (ValidationContext.IsNeedToStop)
                    {
                        break;
                    }

                    foreach (var validationResult in ValidateOnInsert(entity))
                    {
                        yield return validationResult;
                    }
                    break;
                case EntityValidationType.Update:
                    foreach (var validation in ValidationContext.Errors)
                    {
                        yield return validation;
                    }
                    if (ValidationContext.IsNeedToStop)
                    {
                        break;
                    }
                    
                    foreach (var validationResult in ValidateOnInsertOrUpdate(entity))
                    {
                        yield return validationResult;
                    }
                    
                    if (ValidationContext.IsNeedToStop)
                    {
                        break;
                    }

                    foreach (var validationResult in ValidateOnUpdate(entity))
                    {
                        yield return validationResult;
                    }
                    break;
                default:
                    foreach (var validation in ValidationContext.Errors)
                    {
                        yield return validation;
                    }
                    break;
            }
        }

        /// <summary>
        /// Checks entity for errors and returns. Validates entity for business logic rules on Creating operations
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual IEnumerable<ValidationResult> ValidateOnInsert(TEntity entity)
        {
            return new List<ValidationResult>();
        }

        /// <summary>
        /// Checks entity for errors and returns. Validates entity for business logic rules on Editing operations
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual IEnumerable<ValidationResult> ValidateOnUpdate(TEntity entity)
        {
            return new List<ValidationResult>();
        }

        /// <summary>
        /// Returns common validation results for Insert and for Update operations
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual IEnumerable<ValidationResult> ValidateOnInsertOrUpdate(TEntity entity)
        {
            return new List<ValidationResult>();
        }

        /// <inheritdoc />
        public TEntity Find(object property)
        {
            return Repository.Find(property);
        }

        /// <inheritdoc />
        public void AddValidationResult(ValidationResult validationResult)
        {
            ValidationContext.AddValidationResult(validationResult);
        }

        /// <inheritdoc />
        public bool IsNeedToStop {
            get { return ValidationContext.IsNeedToStop; } }

        /// <inheritdoc />
        public ValidationContext ValidationContext { get; set; }
    }
}
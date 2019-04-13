using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calabonga.AspNetCore.Micro.Core;
using Calabonga.AspNetCore.Micro.Core.Exceptions;
using Calabonga.AspNetCore.Micro.Data;
using Calabonga.AspNetCore.Micro.Models.Base;
using $safeprojectname$.Extensions;
using $safeprojectname$.Infrastructure.Factories.Base;
using $safeprojectname$.Infrastructure.Managers.Base;
using $safeprojectname$.Infrastructure.QueryParams;
using $safeprojectname$.Infrastructure.Services;
using $safeprojectname$.Infrastructure.Settings;
using $safeprojectname$.Infrastructure.Validations.Base;
using $safeprojectname$.Infrastructure.ViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.OperationResultsCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace $safeprojectname$.Controllers.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Entity with Unit of Work (CRUD)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TQueryParams"></typeparam>
    /// <typeparam name="TUpdateViewModel"></typeparam>
    /// <typeparam name="TCreateViewModel"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    public abstract class WritableController<TEntity, TCreateViewModel, TUpdateViewModel, TViewModel, TQueryParams>
        : ReadOnlyController<TEntity, TViewModel, TQueryParams>
        where TEntity : Identity
        where TViewModel : ViewModelBase
        where TQueryParams : PagedListQueryParams
        where TCreateViewModel : IViewModel, new()
        where TUpdateViewModel : IViewModel, IHaveId, new()
    {
        /// <inheritdoc />
        protected WritableController(
            IEntityManager<TEntity, TCreateViewModel, TUpdateViewModel> entityManager,
            IOptions<CurrentAppSettings> options,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork,
            IAccountService accountService)
            : base(entityManager.CurrentMapper, options, unitOfWork, accountService)
        {
            EntityManager = entityManager;
        }

        #region Properties

        /// <summary>
        /// EntityManager instance
        /// </summary>
        protected IEntityManager<TEntity, TCreateViewModel, TUpdateViewModel> EntityManager { get; }

        #endregion

        #region PostAsync

        /// <summary>
        /// Returns predefined ViewModel for entity create operation.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-viewmodel-for-creation")]
        public ActionResult<OperationResult<TCreateViewModel>> GetCreateViewModel()
        {
            var model = EntityManager.ViewModelFactory.GenerateForCreate();
            return OperationResultSuccess(model, string.Format(AppData.Messages.ViewModelFactoryGenerationText, EntityManager.GetType().Name));
        }

        /// <summary>
        /// Creates entity. For viewModel generation you can use {get-create-viewmodel} method to
        /// get viewModel with predefined properties
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("post-item")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<OperationResult<TViewModel>>> PostAsync(TCreateViewModel model)
        {
            using (var transaction = await UnitOfWork.BeginTransactionAsync())
            {
                var operationResult = OperationResult.CreateResult<TViewModel>();

                var entity = EntityManager.CurrentMapper.Map<TCreateViewModel, TEntity>(model);

                await UpdateAuditAsync(entity, AccountService);
                if (entity == null)
                {
                    transaction.Rollback();
                    operationResult.AddError(new MicroserviceUnauthorizedException(AppData.Exceptions.UserNotFoundException));
                    return Ok(operationResult);
                }

                EntityManager.OnCreateBeforeAnyValidations(model, entity);

                if (EntityManager.Validator.IsNeedToStop)
                {
                    transaction.Rollback();
                    operationResult.AddError(new MicroserviceEntityValidationException(EntityManager.Validator.ValidationContext.ToString()));
                    return Ok(operationResult);
                }

                ValidateRolesAndRights(entity);

                if (EntityManager.Validator.IsNeedToStop)
                {
                    transaction.Rollback();
                    operationResult.AddError(new MicroserviceUnauthorizedException());
                    return Ok(operationResult);
                }

                var validatorResult = EntityManager.Validator
                    .ValidateByOperationType(EntityValidationType.Insert, entity)
                    .ToList()
                    .GetResult();

                if (!validatorResult.IsValid)
                {
                    transaction.Rollback();
                    operationResult.AddError(new MicroserviceEntityValidationException(validatorResult.ToString()));
                    return Ok(operationResult);
                }

                EntityManager.OnCreateBeforeInsert(model, entity);

                await Repository.InsertAsync(entity);

                await EntityManager.OnCreateBeforeSaveChangesAsync(model, entity);
                
                await UnitOfWork.SaveChangesAsync();

                var lastResult = UnitOfWork.LastSaveChangesResult;
                if (lastResult.IsOk)
                {
                    transaction.Commit();
                    await EntityManager.OnCreateAfterSaveChangesSuccessAsync(new List<TEntity> { entity });
                    operationResult.Result = EntityManager.CurrentMapper.Map<TEntity, TViewModel>(entity);
                    return Ok(operationResult);
                }

                operationResult.Error = lastResult.Exception;
                transaction.Rollback();
                return Ok(operationResult);
            }
        }

        #endregion

        #region PutAsync

        /// <summary>
        /// Returns predefined ViewModel for entity create operation.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-viewmodel-for-editing/{id:guid}")]
        public ActionResult<OperationResult<TUpdateViewModel>> GetUpdateViewModel(Guid id)
        {
            var model = EntityManager.ViewModelFactory.GenerateForUpdate(id);
            return OperationResultSuccess(model, string.Format(AppData.Messages.ViewModelFactoryGenerationText, EntityManager.GetType().Name));
        }

        /// <summary>
        /// Updates entity. For viewModel generation you can use {get-create-viewmodel} method to get viewModel with predefined properties
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<OperationResult<TViewModel>>> PutAsync(Guid id, TUpdateViewModel model)
        {
            using (var transaction = await UnitOfWork.BeginTransactionAsync())
            {
                var operationResult = OperationResult.CreateResult<TViewModel>();
                var entity = FindEntity(id, model);
                if (EntityManager.Validator.IsNeedToStop)
                {
                    transaction.Rollback();
                    operationResult.AddError(new MicroserviceNotFoundException(EntityManager.Validator.ValidationContext.ToString()));
                    return Ok(operationResult);
                }

                await UpdateAuditAsync(entity, AccountService);
                if (entity == null)
                {
                    transaction.Rollback();
                    operationResult.AddError(new MicroserviceUnauthorizedException(AppData.Exceptions.UserNotFoundException));
                    return Ok(operationResult);
                }

                EntityManager.OnEditBeforeMappings(model, entity);
                EntityManager.CurrentMapper.Map(model, entity);
                EntityManager.OnEditBeforeAnyValidations(model, entity);

                if (EntityManager.Validator.IsNeedToStop)
                {
                    transaction.Rollback();
                    operationResult.AddError(new MicroserviceEntityValidationException(EntityManager.Validator.ValidationContext.ToString()));
                    return Ok(operationResult);
                }

                ValidateRolesAndRights(entity);

                if (EntityManager.Validator.IsNeedToStop)
                {
                    transaction.Rollback();
                    operationResult.AddError(new MicroserviceEntityValidationException(EntityManager.Validator.ValidationContext.ToString()));
                    return Ok(operationResult);
                }

                var validatorResult = EntityManager.Validator
                    .ValidateByOperationType(EntityValidationType.Update, entity)
                    .ToList()
                    .GetResult();

                if (!validatorResult.IsValid)
                {
                    transaction.Rollback();
                    operationResult.AddError(new MicroserviceEntityValidationException(validatorResult.ToString()));
                    return Ok(operationResult);
                }

                EntityManager.OnEditBeforeUpdate(model, entity);

                Repository.Update(entity);

                await UnitOfWork.SaveChangesAsync();

                var saveResult = UnitOfWork.LastSaveChangesResult;
                if (saveResult.IsOk)
                {
                    transaction.Commit();
                    operationResult.Result = EntityManager.CurrentMapper.Map<TEntity, TViewModel>(entity);
                    await EntityManager.OnEditAfterSaveChangesSuccessAsync(new List<TEntity> { entity });
                    return Ok(operationResult);
                }

                operationResult.Error = saveResult.Exception;
                transaction.Rollback();
                return Ok(operationResult);
            }
        }

        #endregion

        #region DeleteAsync
        /// <summary>
        /// Deletes entity from repository by identifier and return it as response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<OperationResult<TViewModel>>> DeleteAsync(Guid id)
        {
            var operation = OperationResult.CreateResult<TViewModel>();
            var entity = Repository.Find(id);
            if (entity == null)
            {
                operation.AddError(new MicroserviceNotFoundException());
                return Ok(operation);
            }

            var accessRights = ValidateUserRolesAndRights(entity);
            if (!accessRights.IsOk)
            {
                var adminRights = await AccountService.IsInRolesAsync(new[] { AppData.SystemAdministratorRoleName });
                if (adminRights == null || !adminRights.IsOk)
                {
                    operation.AddError(new UnauthorizedAccessException(accessRights.ToString()));
                    return Ok(operation);
                }
            }

            Repository.Delete(entity);
            await UnitOfWork.SaveChangesAsync();
            if (UnitOfWork.LastSaveChangesResult.IsOk)
            {
                operation.Result = EntityManager.CurrentMapper.Map<TEntity, TViewModel>(entity);
                operation.AddSuccess(AppData.Messages.EntitySuccessfullyDeleted);
                return Ok(operation);
            }
            operation.AddError(UnitOfWork.LastSaveChangesResult.Exception);
            return Ok(operation);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Fills the audit properties
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="accountService"></param>
        /// <returns></returns>
        private async Task UpdateAuditAsync(TEntity entity, IAccountService accountService)
        {
            var user = await accountService.GetCurrentUserAsync();
            if (entity is IAuditable audible)
            {
                var date = DateTime.UtcNow;
                audible.CreatedBy = user?.Email;
                audible.UpdatedBy = user?.Email;
                audible.CreatedAt = date;
                audible.UpdatedAt = date;
            }
        }

        /// <summary>
        /// Returns entity from database by default without any includes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual TEntity FindEntity(Guid id, TUpdateViewModel model)
        {
            var item = Repository.Find(id);
            if (item == null)
            {
                EntityManager.Validator.AddValidationResult(new ValidationResult(AppData.Exceptions.NotFoundException, true));
            }
            return item;
        }


        /// <summary>
        /// Validates user access permissions
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected void ValidateRolesAndRights(TEntity entity)
        {
            var result = ValidateUserRolesAndRights(entity);
            if (result.IsOk)
            {
                return;
            }

            foreach (var validationResult in result.Errors)
            {
                EntityManager.Validator.AddValidationResult(validationResult);
            }
        }
        #endregion
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.AspNetCore.Micro.Entities.Base;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Factories.Base;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Validations.Base;

namespace Calabonga.AspNetCore.Micro.Web.Infrastructure.Managers.Base
{
    /// <summary>
    /// Entity Manager
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TCreateViewModel"></typeparam>
    /// <typeparam name="TUpdateViewModel"></typeparam>
    public interface IEntityManager<TEntity, TCreateViewModel, TUpdateViewModel>
        where TCreateViewModel : IViewModel, new()
        where TUpdateViewModel : IViewModel, new()
        where TEntity : class, IHaveId
    {
        /// <summary>
        /// <see cref="IMapper"/> instance for current manager
        /// </summary>
        IMapper CurrentMapper { get; }

        /// <summary>
        /// ViewModel factory can help you to instantiate view models with initial data
        /// </summary>
        IViewModelFactory<TEntity, TCreateViewModel, TUpdateViewModel> ViewModelFactory { get; }

        /// <summary>
        /// Entity validator for current entity type. It can contains custom set of rules for entity validations
        /// </summary>
        IEntityValidator<TEntity> Validator { get; }

        #region OnCreate Handlers

        /// <summary>
        /// Fires when validation already complete and next step is saving entity on insert operations
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        void OnCreateBeforeInsert(TCreateViewModel model, TEntity entity);

        /// <summary>
        /// Fires when entity ready to Insert but some operations still need to do before saveChanges executed
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        Task OnCreateBeforeSaveChangesAsync(TCreateViewModel model, TEntity entity);

        /// <summary>
        /// Fires after changes successfully saved
        /// </summary>
        /// <param name="entities"></param>
        Task OnCreateAfterSaveChangesSuccessAsync(List<TEntity> entities);

        /// <summary>
        /// Fires before entity validation executed on entity creation
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        void OnCreateBeforeAnyValidations(TCreateViewModel model, TEntity entity);

        #endregion

        #region OnEdit Handlers

        /// <summary>
        /// Fires after changes successfully saved
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task OnEditAfterSaveChangesSuccessAsync(List<TEntity> entities);

        /// <summary>
        /// Fires when validation already complete and next step is saving entity on update operations
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        void OnEditBeforeUpdate(TUpdateViewModel model, TEntity entity);

        /// <summary>
        /// Fires before entity validation executed on entity updating
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        void OnEditBeforeAnyValidations(TUpdateViewModel model, TEntity entity);

        /// <summary>
        /// Fires before any mapping operations begin
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        void OnEditBeforeMappings(TUpdateViewModel model, TEntity entity);

        #endregion
    }
}
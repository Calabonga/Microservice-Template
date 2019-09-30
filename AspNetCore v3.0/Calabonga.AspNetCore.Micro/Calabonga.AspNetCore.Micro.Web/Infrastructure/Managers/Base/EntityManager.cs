using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.AspNetCore.Micro.Entities.Base;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Factories.Base;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Validations.Base;

namespace Calabonga.AspNetCore.Micro.Web.Infrastructure.Managers.Base
{
    /// <summary>
    /// Entity manager represents a set of the tools for entity management
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TUpdateViewModel"></typeparam>
    /// <typeparam name="TCreateViewModel"></typeparam>
    public abstract class EntityManager<TEntity, TCreateViewModel, TUpdateViewModel>
        : IEntityManager<TEntity, TCreateViewModel, TUpdateViewModel>
        where TEntity : class, IHaveId
        where TCreateViewModel : IViewModel, new()
        where TUpdateViewModel : IViewModel, new()
    {
        /// <inheritdoc />
        protected EntityManager(
            IMapper mapper,
            IViewModelFactory<TEntity, TCreateViewModel, TUpdateViewModel> viewModelFactory,
            IEntityValidator<TEntity> validator)
        {
            CurrentMapper = mapper;
            ViewModelFactory = viewModelFactory;
            Validator = validator;
        }

        /// <summary>
        /// <see cref="IMapper"/> instance for current manager
        /// </summary>
        public IMapper CurrentMapper { get; }

        /// <summary>
        /// ViewModel factory can help you to instantiate view models with initial data
        /// </summary>
        public IViewModelFactory<TEntity, TCreateViewModel, TUpdateViewModel> ViewModelFactory { get; }

        /// <summary>
        /// Entity validator for current entity type. It can contains custom set of rules for entity validations
        /// </summary>
        public IEntityValidator<TEntity> Validator { get; }

        #region OnCreated Handlers

        /// <inheritdoc />
        public virtual void OnCreateBeforeAnyValidations(TCreateViewModel model, TEntity entity) { }

        /// <inheritdoc />
        public virtual void OnCreateBeforeInsert(TCreateViewModel model, TEntity entity) { }

        /// <inheritdoc />
        public virtual Task OnCreateBeforeSaveChangesAsync(TCreateViewModel model, TEntity entity)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task OnCreateAfterSaveChangesSuccessAsync(List<TEntity> entities)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region OnEdit Handlers

     

        /// <inheritdoc />
        public virtual void OnEditBeforeUpdate(TUpdateViewModel model, TEntity entity) { }

        /// <inheritdoc />
        public virtual void OnEditBeforeAnyValidations(TUpdateViewModel model, TEntity entity) { }

        /// <inheritdoc />
        public virtual void OnEditBeforeMappings(TUpdateViewModel model, TEntity entity) { }

        /// <inheritdoc />
        public virtual Task OnEditAfterSaveChangesSuccessAsync(List<TEntity> entities)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
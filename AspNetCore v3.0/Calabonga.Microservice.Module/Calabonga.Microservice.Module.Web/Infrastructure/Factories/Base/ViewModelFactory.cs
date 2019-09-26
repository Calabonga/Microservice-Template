using System;
using Calabonga.Microservice.Module.Models.Base;

namespace Calabonga.Microservice.Module.Web.Infrastructure.Factories.Base
{
    /// <summary>
    /// ViewModelFactory base class
    /// </summary>
    /// <typeparam name="TUpdateViewModel"></typeparam>
    /// <typeparam name="TCreateViewModel"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class ViewModelFactory<TEntity, TCreateViewModel, TUpdateViewModel> : IViewModelFactory<TEntity, TCreateViewModel, TUpdateViewModel>
        where TCreateViewModel : IViewModel, new()
        where TUpdateViewModel : IViewModel, new()
        where TEntity : class, IHaveId
    {

        /// <inheritdoc />
        public abstract TCreateViewModel GenerateForCreate();

        /// <inheritdoc />
        public abstract TUpdateViewModel GenerateForUpdate(Guid id);
    }
}
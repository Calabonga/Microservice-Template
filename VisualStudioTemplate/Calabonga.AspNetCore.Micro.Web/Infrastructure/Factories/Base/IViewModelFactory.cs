using System;
using Calabonga.AspNetCore.Micro.Models.Base;

namespace $safeprojectname$.Infrastructure.Factories.Base
{
    /// <summary>
    /// ViewModel Factory
    /// </summary>
    /// <typeparam name="TUpdateViewModel"></typeparam>
    /// <typeparam name="TCreateViewModel"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IViewModelFactory<TEntity, out TCreateViewModel, out TUpdateViewModel>
        where TCreateViewModel : IViewModel, new()
        where TUpdateViewModel : IViewModel, new()
        where TEntity : class, IHaveId
    {
        /// <summary>
        /// Returns ViewModel for entity creation
        /// </summary>
        /// <returns></returns>
        TCreateViewModel GenerateForCreate();

        /// <summary>
        /// Returns ViewModel for entity editing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TUpdateViewModel GenerateForUpdate(Guid id);
    }
}
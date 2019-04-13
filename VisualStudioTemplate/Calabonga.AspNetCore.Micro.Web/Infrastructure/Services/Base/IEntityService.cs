using System;

namespace $safeprojectname$.Infrastructure.Services.Base
{
    /// <summary>
    /// Entity service
    /// </summary>
    public interface IEntityService<out T>
    {
        /// <summary>
        /// Returns object by his identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindEntity(Guid id);
    }
}
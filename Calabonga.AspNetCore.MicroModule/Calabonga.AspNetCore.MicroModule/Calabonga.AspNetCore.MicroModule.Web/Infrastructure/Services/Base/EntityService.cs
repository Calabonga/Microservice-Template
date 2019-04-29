using Calabonga.EntityFrameworkCore.UOW;

namespace Calabonga.AspNetCore.MicroModule.Web.Infrastructure.Services.Base
{
    /// <summary>
    /// Base service for entity with default predefined actions (methods)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EntityService<TEntity> where TEntity : class
    {

        /// <inheritdoc />
        protected EntityService(IRepositoryFactory factory)
        {
            Factory = factory;
        }

        /// <summary>
        /// Factory of Repositories
        /// </summary>
        protected IRepositoryFactory Factory { get; }

        /// <summary>
        /// Repository
        /// </summary>
        protected IRepository<TEntity> Repository
        {
            get { return Factory.GetRepository<TEntity>(); }
        }
    }
}
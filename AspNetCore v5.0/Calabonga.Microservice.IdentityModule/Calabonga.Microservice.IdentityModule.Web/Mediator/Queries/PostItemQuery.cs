using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.OperationResults;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Queries
{
    /// <summary>
    /// Query for Post Item operation
    /// </summary>
    public abstract class PostItemQuery<TEntity, TViewModel, TCreateViewModel> : RequestBase<OperationResult<TViewModel>>, IViewModel
        where TEntity : Identity
        where TViewModel : ViewModelBase
        where TCreateViewModel : IViewModel
    {
        protected PostItemQuery(TCreateViewModel model) => CreateViewModel = model;

        /// <summary>
        /// ViewModel for create operation
        /// </summary>
        public TCreateViewModel CreateViewModel { get; }
    }
}
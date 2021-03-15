using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.OperationResults;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Queries
{
    /// <summary>
    /// Query for Log creation
    /// </summary>
    public abstract class PutItemQuery<TEntity, TViewModel, TUpdateViewModel> : RequestBase<OperationResult<TViewModel>>, IViewModel
        where TEntity : Identity
        where TViewModel : ViewModelBase
        where TUpdateViewModel: ViewModelBase
    {
        protected PutItemQuery(TUpdateViewModel model) => UpdateViewModel = model;

        /// <summary>
        /// ViewModel for update operation
        /// </summary>
        public TUpdateViewModel UpdateViewModel { get; }

    }
}
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.OperationResults;
using System;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Queries
{
    /// <summary>
    /// Deletes entity by identifier
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class DeleteByIdQuery<TEntity, TViewModel> :  RequestBase<OperationResult<TViewModel>>, IViewModel
        where TEntity : Identity
        where TViewModel : ViewModelBase
    {
        protected DeleteByIdQuery(Guid id) => Id = id;

        public Guid Id { get; }

  
    }
}
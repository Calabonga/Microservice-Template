using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.OperationResults;
using System;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Queries
{
    /// <summary>
    /// Query for UpdateViewModel getting operation
    /// </summary>
    /// <typeparam name="TUpdateViewModel"></typeparam>
    public abstract class UpdateViewModelQuery<TUpdateViewModel> : RequestBase<OperationResult<TUpdateViewModel>>
        where TUpdateViewModel : ViewModelBase
    {
        protected UpdateViewModelQuery(Guid id) => Id = id;

        public Guid Id { get; }
    }
}
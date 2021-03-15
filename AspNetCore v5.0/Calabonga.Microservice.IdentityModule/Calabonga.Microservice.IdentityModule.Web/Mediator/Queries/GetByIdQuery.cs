using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.OperationResults;
using System;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Queries
{
    /// <summary>
    /// Query for Log
    /// </summary>
    public abstract class GetByIdQuery<TViewModel> : RequestBase<OperationResult<TViewModel>>
        where TViewModel : ViewModelBase
    {
        protected GetByIdQuery(Guid id) => Id = id;

        public Guid Id { get; }
    }
}
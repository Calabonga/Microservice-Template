using System;
using AutoMapper;
using Calabonga.Microservice.IdentityModule.Entities;
using Calabonga.Microservice.IdentityModule.Web.Mediator.Handlers;
using Calabonga.Microservice.IdentityModule.Web.Mediator.Queries;
using Calabonga.Microservice.IdentityModule.Web.ViewModels.LogViewModels;
using Calabonga.UnitOfWork;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.LogsWritable
{
    /// <summary>
    /// Request: Log delete
    /// </summary>
    public class LogDeleteItemRequest: DeleteByIdQuery<Log, LogViewModel>
    {
        public LogDeleteItemRequest(Guid id) : base(id)
        {
        }
    }

    /// <summary>
    /// Request: Log delete
    /// </summary>
    public class LogDeleteItemRequestHandler : DeleteByIdHandlerBase<Log, LogViewModel>
    {
        public LogDeleteItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}

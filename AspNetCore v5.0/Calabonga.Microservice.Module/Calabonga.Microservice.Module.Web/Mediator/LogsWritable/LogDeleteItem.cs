using System;
using AutoMapper;
using Calabonga.AspNetCore.Controllers.Handlers;
using Calabonga.AspNetCore.Controllers.Queries;
using Calabonga.Microservice.Module.Entities;
using Calabonga.Microservice.Module.Web.ViewModels.LogViewModels;
using Calabonga.UnitOfWork;

namespace Calabonga.Microservice.Module.Web.Mediator.LogsWritable
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

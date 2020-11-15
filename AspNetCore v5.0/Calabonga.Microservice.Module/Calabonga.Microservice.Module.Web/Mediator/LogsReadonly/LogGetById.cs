using System;
using AutoMapper;
using Calabonga.AspNetCore.Controllers.Handlers;
using Calabonga.AspNetCore.Controllers.Queries;
using Calabonga.Microservice.Module.Entities;
using Calabonga.Microservice.Module.Web.ViewModels.LogViewModels;
using Calabonga.UnitOfWork;

namespace Calabonga.Microservice.Module.Web.Mediator.LogsReadonly
{
    /// <summary>
    /// Request for Log by Identifier
    /// </summary>
    public class LogGetByIdRequest : GetByIdQuery<LogViewModel>
    {
        public LogGetByIdRequest(Guid id) : base(id)
        {
        }
    }

    /// <summary>
    /// Response for  Request for Log by Identifier
    /// </summary>
    public class LogGetByIdRequestHandler : GetByIdHandlerBase<LogGetByIdRequest, Log, LogViewModel>
    {
        public LogGetByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}

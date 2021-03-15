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
    /// Request: Returns ViewModel for entity Log creation
    /// </summary>
    public class LogUpdateViewModelRequest: UpdateViewModelQuery<LogUpdateViewModel>
    {
        public LogUpdateViewModelRequest(Guid id) : base(id)
        {
        }
    }

    /// <summary>
    /// Response: Returns ViewModel for entity Log creation
    /// </summary>
    public class LogUpdateViewModelRequestHandler : UpdateViewModelHandlerBase<LogUpdateViewModelRequest, Log, LogUpdateViewModel>
    {
        public LogUpdateViewModelRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}

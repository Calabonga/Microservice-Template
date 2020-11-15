using System;
using AutoMapper;
using Calabonga.AspNetCore.Controllers.Handlers;
using Calabonga.AspNetCore.Controllers.Queries;
using $ext_projectname$.Entities;
using $safeprojectname$.ViewModels.LogViewModels;
using Calabonga.UnitOfWork;

namespace $safeprojectname$.Mediator.LogsWritable
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

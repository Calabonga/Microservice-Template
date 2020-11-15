using AutoMapper;

using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Handlers;
using Calabonga.AspNetCore.Controllers.Queries;
using $ext_projectname$.Entities;
using $safeprojectname$.Infrastructure.EventLogs;
using $safeprojectname$.ViewModels.LogViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;

using Microsoft.Extensions.Logging;

namespace $safeprojectname$.Mediator.LogsWritable
{
    /// <summary>
    /// Request: Log creation
    /// </summary>
    public class LogPostItemRequest : PostItemQuery<Log, LogViewModel, LogCreateViewModel>
    {
        public LogPostItemRequest(LogCreateViewModel model) : base(model)
        {
        }
    }

    /// <summary>
    /// Request: Log creation
    /// </summary>
    public class LogPostItemRequestHandler : PostItemHandlerBase<Log, LogViewModel, LogCreateViewModel>
    {
        private readonly ILogger<LogPostItemRequestHandler> _logger;


        public LogPostItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<LogPostItemRequestHandler> logger)
            : base(unitOfWork, mapper)
        {
            _logger = logger;
        }

        protected override OperationResult<LogViewModel> OperationResultBeforeReturn(HandlerStep step, OperationResult<LogViewModel> operationResult)
        {
            if (operationResult.Ok)
            {
                _logger.MicroservicePostItem(nameof(Log));
            }
            else
            {
                _logger.MicroservicePostItem(nameof(Entities.Log), operationResult.Exception);
            }
            return base.OperationResultBeforeReturn(step, operationResult);
        }
    }
}

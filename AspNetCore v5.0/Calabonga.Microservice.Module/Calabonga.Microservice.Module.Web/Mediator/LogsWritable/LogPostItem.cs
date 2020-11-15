using AutoMapper;

using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Handlers;
using Calabonga.AspNetCore.Controllers.Queries;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.EventLogs;
using Calabonga.Microservice.Module.Entities;
using Calabonga.Microservice.Module.Web.ViewModels.LogViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;

using Microsoft.Extensions.Logging;

namespace Calabonga.Microservice.Module.Web.Mediator.LogsWritable
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
                _logger.MicroservicePostItem(nameof(Log), operationResult.Exception);
            }

            return base.OperationResultBeforeReturn(step, operationResult);
        }
    }
}

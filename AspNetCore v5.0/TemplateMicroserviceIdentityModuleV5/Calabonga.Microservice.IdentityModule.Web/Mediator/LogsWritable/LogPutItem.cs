using AutoMapper;
using Calabonga.AspNetCore.Controllers.Handlers;
using Calabonga.AspNetCore.Controllers.Queries;
using $ext_projectname$.Entities;
using $safeprojectname$.ViewModels.LogViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;

namespace $safeprojectname$.Mediator.LogsWritable
{
    /// <summary>
    /// Request: Log edit
    /// </summary>
    public class LogPutItemRequest: PutItemQuery<Log, LogViewModel, LogUpdateViewModel>
    {
        public LogPutItemRequest(LogUpdateViewModel model) : base(model)
        {
        }
    }

    /// <summary>
    /// Request: Log creation
    /// </summary>
    public class LogPutItemRequestHandler : PutItemHandlerBase<Log, LogViewModel, LogUpdateViewModel>
    {
        public LogPutItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override OperationResult<LogViewModel> ProcessOperationResult(OperationResult<LogViewModel> operationResult, LogViewModel response)
        {
            operationResult.AppendLog("Successfully updated");
            operationResult.Result = response;
            return operationResult;
        }
    }
}

using AutoMapper;
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.Microservice.IdentityModule.Entities;
using Calabonga.Microservice.IdentityModule.Web.ViewModels.LogViewModels;
using Calabonga.Microservices.Core;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;

using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.LogsWritable
{
    /// <summary>
    /// Request: Log creation
    /// </summary>
    public class LogPostItemRequest : OperationResultRequestBase<LogViewModel>
    {
        public LogPostItemRequest(LogCreateViewModel model)
        {
            Model = model;
        }

        public LogCreateViewModel Model { get; }
    }

    /// <summary>
    /// Request: Log creation
    /// </summary>
    public class LogPostItemRequestHandler : OperationResultRequestHandlerBase<LogPostItemRequest, LogViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<LogPostItemRequestHandler> _logger;


        public LogPostItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<LogPostItemRequestHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<OperationResult<LogViewModel>> Handle(LogPostItemRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<LogViewModel>();
            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            var entity = _mapper.Map<LogCreateViewModel, Log>(request.Model);
            if (entity == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                operation.AddError(new MicroserviceUnauthorizedException(AppContracts.Exceptions.MappingException));
                return operation;
            }
            
            await _unitOfWork.GetRepository<Log>().InsertAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var lastResult = _unitOfWork.LastSaveChangesResult;
            if (lastResult.IsOk)
            {

                var mapped = _mapper.Map<Log, LogViewModel>(entity);

                operation.Result = mapped;
                operation.AddSuccess("Successfully created");
                return operation;
            }

            await transaction.RollbackAsync(cancellationToken);
            operation.AddError(lastResult.Exception);
            operation.AppendLog("Something went wrong");
            return operation;
        }
    }
}

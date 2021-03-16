using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.AspNetCore.Controllers.Base;
using $ext_projectname$.Entities;
using $safeprojectname$.ViewModels.LogViewModels;
using Calabonga.Microservices.Core;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;

namespace $safeprojectname$.Mediator.LogsWritable
{
    /// <summary>
    /// Request: Log edit
    /// </summary>
    public class LogPutItemRequest : OperationResultRequestBase<LogViewModel>
    {
        public LogPutItemRequest(LogUpdateViewModel model)
        {
            Model = model;
        }

        public LogUpdateViewModel Model { get; }
    }

    /// <summary>
    /// Request: Log creation
    /// </summary>
    public class LogPutItemRequestHandler : OperationResultRequestHandlerBase<LogPutItemRequest, LogViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LogPutItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task<OperationResult<LogViewModel>> Handle(LogPutItemRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<LogViewModel>();
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            var repository = _unitOfWork.GetRepository<Log>();
            var entity = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Model.Id, disableTracking: false);
            if (entity == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                operation.AddError(new MicroserviceNotFoundException(AppContracts.Exceptions.NotFoundException));
                return operation;
            }
            
            _mapper.Map(request.Model, entity);

            repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var lastResult = _unitOfWork.LastSaveChangesResult;
            if (lastResult.IsOk)
            {
                var mapped = _mapper.Map<Log, LogViewModel>(entity);
                operation.Result = mapped;
                operation.AddSuccess("Successfully updated");
                return operation;
            }

            await transaction.RollbackAsync(cancellationToken);
            operation.AddError(lastResult.Exception);
            operation.AppendLog("Something went wrong");
            return operation;
        }
    }
}

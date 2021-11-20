using AutoMapper;
using $ext_projectname$.Domain;
using $safeprojectname$.Endpoints.LogsEndpoints.ViewModels;
using Calabonga.Microservices.Core;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace $safeprojectname$.Endpoints.LogsEndpoints.Queries
{
    /// <summary>
    /// Request: Log edit
    /// </summary>
    public record LogPutRequest(Guid Id, LogUpdateViewModel Model) : IRequest<OperationResult<LogViewModel>>;

    /// <summary>
    /// Request: Log creation
    /// </summary>
    public class LogPutItemRequestHandler : IRequestHandler<LogPutRequest, OperationResult<LogViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LogPutItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<LogViewModel>> Handle(LogPutRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<LogViewModel>();
            var repository = _unitOfWork.GetRepository<Log>();
            var entity = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id, disableTracking: false);
            if (entity == null)
            {
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

            operation.AddError(lastResult.Exception);
            operation.AppendLog("Something went wrong");
            return operation;
        }
    }
}

using AutoMapper;
using $ext_projectname$.Domain;
using $safeprojectname$.Endpoints.LogsEndpoints.ViewModels;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace $safeprojectname$.Endpoints.LogsEndpoints.Queries;

/// <summary>
/// Request: Log delete
/// </summary>
public record LogDeleteItemRequest(Guid Id) : IRequest<OperationResult<LogViewModel>>;

/// <summary>
/// Request: Log delete
/// </summary>
public class LogDeleteItemRequestHandler :  IRequestHandler<LogDeleteItemRequest, OperationResult<LogViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public LogDeleteItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<OperationResult<LogViewModel>> Handle(LogDeleteItemRequest request, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<LogViewModel>();
        var repository = _unitOfWork.GetRepository<Log>();
        var entity = await repository.FindAsync(request.Id);
        if (entity == null)
        {
            operation.AddError(new MicroserviceNotFoundException("Entity not found"));
            return operation;
        }
        repository.Delete(entity);
        await _unitOfWork.SaveChangesAsync();
        if (_unitOfWork.LastSaveChangesResult.IsOk)
        {
            operation.Result = _mapper.Map<LogViewModel>(entity);
            return operation;
        }
        operation.AddError(_unitOfWork.LastSaveChangesResult.Exception);
        return operation;
    }
}
using AutoMapper;
using $ext_projectname$.Domain;
using $safeprojectname$.Endpoints.LogsEndpoints.ViewModels;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace $safeprojectname$.Endpoints.LogsEndpoints.Queries;

/// <summary>
/// Request for Log by Identifier
/// </summary>
public record LogGetByIdRequest(Guid Id) : IRequest<OperationResult<LogViewModel>>;

/// <summary>
/// Response for  Request for Log by Identifier
/// </summary>
public class LogGetByIdRequestHandler : IRequestHandler<LogGetByIdRequest, OperationResult<LogViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LogGetByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>Handles a request getting log by identifier</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<OperationResult<LogViewModel>> Handle(LogGetByIdRequest request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var operation = OperationResult.CreateResult<LogViewModel>();
        var repository = _unitOfWork.GetRepository<Log>();
        var entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == id);
        if (entityWithoutIncludes == null)
        {
            operation.AddError(new MicroserviceNotFoundException($"Entity with identifier {id} not found"));
            return operation;
        }
        operation.Result = _mapper.Map<LogViewModel>(entityWithoutIncludes);
        return operation;
    }
}
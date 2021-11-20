using AutoMapper;
using $ext_projectname$.Domain;
using $safeprojectname$.Endpoints.LogsEndpoints.ViewModels;
using Calabonga.Microservices.Core;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace $safeprojectname$.Endpoints.LogsEndpoints.Queries;

/// <summary>
/// Request: Log creation
/// </summary>
public record LogPostRequest(LogCreateViewModel Model) : IRequest<OperationResult<LogViewModel>>;

/// <summary>
/// Request: Log creation
/// </summary>
public class LogPostRequestHandler : IRequestHandler<LogPostRequest, OperationResult<LogViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<LogPostRequestHandler> _logger;


    public LogPostRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<LogPostRequestHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<OperationResult<LogViewModel>> Handle(LogPostRequest request, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<LogViewModel>();

        var entity = _mapper.Map<LogCreateViewModel, Log>(request.Model);
        if (entity == null)
        {
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

        operation.AddError(lastResult.Exception);
        operation.AppendLog("Something went wrong");
        
        return operation;
    }
}
using AutoMapper;
using $ext_projectname$.Domain;
using $safeprojectname$.Endpoints.EventItemsEndpoints.ViewModels;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace $safeprojectname$.Endpoints.EventItemsEndpoints.Queries;

/// <summary>
/// Request: EventItem delete
/// </summary>
public record DeleteEventItemRequest(Guid Id) : IRequest<OperationResult<EventItemViewModel>>;

/// <summary>
/// Request: EventItem delete
/// </summary>
public class DeleteEventItemRequestHandler : IRequestHandler<DeleteEventItemRequest, OperationResult<EventItemViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public DeleteEventItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<OperationResult<EventItemViewModel>> Handle(DeleteEventItemRequest request, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<EventItemViewModel>();
        var repository = _unitOfWork.GetRepository<EventItem>();
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
            operation.Result = _mapper.Map<EventItemViewModel>(entity);
            return operation;
        }
        operation.AddError(_unitOfWork.LastSaveChangesResult.Exception);
        return operation;
    }
}
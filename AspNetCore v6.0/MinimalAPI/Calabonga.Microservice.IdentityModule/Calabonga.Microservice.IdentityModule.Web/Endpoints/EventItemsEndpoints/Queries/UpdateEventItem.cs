using AutoMapper;
using Calabonga.Microservice.IdentityModule.Domain;
using Calabonga.Microservice.IdentityModule.Web.Endpoints.EventItemsEndpoints.ViewModels;
using Calabonga.Microservices.Core;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints.EventItemsEndpoints.Queries;

/// <summary>
/// Request: EventItem edit
/// </summary>
public record PutEventItemRequest(Guid Id, EventItemUpdateViewModel Model) : IRequest<OperationResult<EventItemViewModel>>;

/// <summary>
/// Request: EventItem creation
/// </summary>
public class PutEventItemRequestHandler : IRequestHandler<PutEventItemRequest, OperationResult<EventItemViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PutEventItemRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationResult<EventItemViewModel>> Handle(PutEventItemRequest eventItemRequest, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<EventItemViewModel>();
        var repository = _unitOfWork.GetRepository<EventItem>();
        var entity = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == eventItemRequest.Id, disableTracking: false);
        if (entity == null)
        {
            operation.AddError(new MicroserviceNotFoundException(AppContracts.Exceptions.NotFoundException));
            return operation;
        }

        _mapper.Map(eventItemRequest.Model, entity);

        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        var lastResult = _unitOfWork.LastSaveChangesResult;
        if (lastResult.IsOk)
        {
            var mapped = _mapper.Map<EventItem, EventItemViewModel>(entity);
            operation.Result = mapped;
            operation.AddSuccess("Successfully updated");
            return operation;
        }

        operation.AddError(lastResult.Exception);
        operation.AppendLog("Something went wrong");
        return operation;
    }
}
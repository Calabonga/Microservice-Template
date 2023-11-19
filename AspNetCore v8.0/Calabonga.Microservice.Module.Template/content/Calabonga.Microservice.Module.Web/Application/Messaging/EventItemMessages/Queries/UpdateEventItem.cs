using AutoMapper;
using Calabonga.Microservice.Module.Domain;
using Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.Microservices.Core;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
/// Request: EventItem edit
/// </summary>
public sealed class PutEventItem
{
    public record Request(Guid Id, EventItemUpdateViewModel Model) : IRequest<OperationResult<EventItemViewModel>>;

    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, OperationResult<EventItemViewModel>>
    {
        public async Task<OperationResult<EventItemViewModel>> Handle(Request eventItemRequest, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<EventItemViewModel>();
            var repository = unitOfWork.GetRepository<EventItem>();
            var entity = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == eventItemRequest.Id, disableTracking: false);
            if (entity == null)
            {
                operation.AddError(new MicroserviceNotFoundException(AppContracts.Exceptions.NotFoundException));
                return operation;
            }

            mapper.Map(eventItemRequest.Model, entity);

            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();

            var lastResult = unitOfWork.LastSaveChangesResult;
            if (lastResult.IsOk)
            {
                var mapped = mapper.Map<EventItem, EventItemViewModel>(entity);
                operation.Result = mapped;
                operation.AddSuccess("Successfully updated");
                return operation;
            }

            operation.AddError(lastResult.Exception ?? new ApplicationException("Something went wrong"));
            return operation;
        }
    }
}
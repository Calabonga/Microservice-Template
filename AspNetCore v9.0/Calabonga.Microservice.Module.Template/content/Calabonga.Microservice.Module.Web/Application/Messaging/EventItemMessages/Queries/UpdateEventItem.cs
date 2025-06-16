using Calabonga.Microservice.Module.Domain;
using Calabonga.Microservice.Module.Domain.Base;
using Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.Microservices.Core;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Mediator;

namespace Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
/// Request: EventItem edit
/// </summary>
public static class PutEventItem
{
    public record Request(Guid Id, EventItemUpdateViewModel Model) : IRequest<Operation<EventItemViewModel, string>>;

    public class Handler(IUnitOfWork unitOfWork)
        : IRequestHandler<Request, Operation<EventItemViewModel, string>>
    {
        public async ValueTask<Operation<EventItemViewModel, string>> Handle(Request eventItemRequest, CancellationToken cancellationToken)
        {
            var repository = unitOfWork.GetRepository<EventItem>();
            var entity = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == eventItemRequest.Id, trackingType: TrackingType.Tracking);
            if (entity == null)
            {
                return Operation.Error(AppContracts.Exceptions.NotFoundException);
            }

            entity.MapUpdatesFrom(eventItemRequest.Model);

            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();

            var lastResult = unitOfWork.Result;
            if (lastResult.Ok)
            {
                var mapped = entity.MapToViewModel();
                if (mapped is not null)
                {
                    return Operation.Result(mapped);
                }

                return Operation.Error(AppData.Exceptions.MappingException);
            }

            var errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            return Operation.Error(errorMessage);
        }
    }
}

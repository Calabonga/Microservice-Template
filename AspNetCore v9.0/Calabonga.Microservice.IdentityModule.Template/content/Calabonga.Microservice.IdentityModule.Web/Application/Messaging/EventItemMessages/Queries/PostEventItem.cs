using Calabonga.Microservice.IdentityModule.Domain;
using Calabonga.Microservice.IdentityModule.Domain.Base;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.Microservices.Core;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Mediator;

namespace Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
/// Request: EventItem creation
/// </summary>
public static class PostEventItem
{
    public record Request(EventItemCreateViewModel Model) : IRequest<Operation<EventItemViewModel, string>>;

    public class Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger)
        : IRequestHandler<Request, Operation<EventItemViewModel, string>>
    {
        public async ValueTask<Operation<EventItemViewModel, string>> Handle(Request eventItemRequest, CancellationToken cancellationToken)
        {
            logger.LogDebug("Creating new EventItem");


            var entity = eventItemRequest.Model.MapToEventItem();
            if (entity == null)
            {
                logger.LogError("Mapper not configured correctly or something went wrong");
                return Operation.Error(AppContracts.Exceptions.MappingException);
            }

            await unitOfWork.GetRepository<EventItem>().InsertAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            var lastResult = unitOfWork.Result;
            if (lastResult.Ok)
            {
                var mapped = entity.MapToViewModel();
                if (mapped is null)
                {
                    return Operation.Error(AppData.Exceptions.MappingException);
                }

                logger.LogInformation("New entity {@EventItem} successfully created", entity);
                return Operation.Result(mapped);
            }

            var errorMessage = lastResult.Exception?.Message ?? "Something went wrong";
            logger.LogError(errorMessage);
            return Operation.Error(errorMessage);
        }
    }
}

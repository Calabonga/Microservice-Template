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
/// Request: EventItem creation
/// </summary>
public sealed class PostEventItem
{
    public record Request(EventItemCreateViewModel Model) : IRequest<OperationResult<EventItemViewModel>>;

    public class Handler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Handler> logger)
        : IRequestHandler<Request, OperationResult<EventItemViewModel>>
    {
        public async Task<OperationResult<EventItemViewModel>> Handle(Request eventItemRequest, CancellationToken cancellationToken)
        {
            logger.LogDebug("Creating new EventItem");

            var operation = OperationResult.CreateResult<EventItemViewModel>();

            var entity = mapper.Map<EventItemCreateViewModel, EventItem>(eventItemRequest.Model);
            if (entity == null)
            {
                var exceptionMapper = new MicroserviceUnauthorizedException(AppContracts.Exceptions.MappingException);
                operation.AddError(exceptionMapper);
                logger.LogError(exceptionMapper, "Mapper not configured correctly or something went wrong");
                return operation;
            }

            await unitOfWork.GetRepository<EventItem>().InsertAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            var lastResult = unitOfWork.LastSaveChangesResult;
            if (lastResult.IsOk)
            {
                var mapped = mapper.Map<EventItem, EventItemViewModel>(entity);
                operation.Result = mapped;
                operation.AddSuccess("Successfully created");
                logger.LogInformation("New entity {@EventItem} successfully created", entity);
                return operation;
            }

            var exception = lastResult.Exception ?? new ApplicationException("Something went wrong");
            operation.AddError(exception);
            logger.LogError(exception, "Error data saving to Database or something went wrong");

            return operation;
        }
    }
}
﻿using Calabonga.Microservice.Module.Domain;
using Calabonga.Microservice.Module.Domain.Base;
using Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Mediator;

namespace Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
/// EventItem by Identifier
/// </summary>
public static class GetEventItemById
{
    public record Request(Guid Id) : IRequest<Operation<EventItemViewModel, string>>;

    public class Handler(IUnitOfWork unitOfWork)
        : IRequestHandler<Request, Operation<EventItemViewModel, string>>
    {
        /// <summary>Handles a request getting log by identifier</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async ValueTask<Operation<EventItemViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            var repository = unitOfWork.GetRepository<EventItem>();
            var entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == id, trackingType: TrackingType.NoTracking);
            if (entityWithoutIncludes == null)
            {
                return Operation.Error($"Entity with identifier {id} not found");
            }

            var mapped = entityWithoutIncludes.MapToViewModel();
            if (mapped is not null)
            {
                return Operation.Result(mapped);
            }

            return Operation.Error(AppData.Exceptions.MappingException);
        }
    }
}

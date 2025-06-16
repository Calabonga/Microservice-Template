using AutoMapper;
using Calabonga.Microservice.Module.Domain;
using Calabonga.Microservice.Module.Domain.Base;
using Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Mediator;

namespace Calabonga.Microservice.Module.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
/// Request: EventItem delete
/// </summary>
public static class DeleteEventItem
{
    public record Request(Guid Id) : IRequest<Operation<EventItemViewModel, string>>;

    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<EventItemViewModel, string>>
    {
        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async ValueTask<Operation<EventItemViewModel, string>> Handle(Request request, CancellationToken cancellationToken)
        {
            var repository = unitOfWork.GetRepository<EventItem>();
            var entity = await repository.FindAsync(request.Id);
            if (entity == null)
            {
                return Operation.Error(AppData.Exceptions.NotFoundException);
            }

            repository.Delete(entity);
            await unitOfWork.SaveChangesAsync();
            if (!unitOfWork.Result.Ok)
            {
                return Operation.Error(unitOfWork.Result.Exception?.Message ?? AppData.Exceptions.SomethingWrong);
            }

            var mapped = mapper.Map<EventItemViewModel>(entity);

            return mapped is not null
                ? Operation.Result(mapped)
                : Operation.Error(AppData.Exceptions.MappingException);
        }
    }
}

using AutoMapper;
using Calabonga.Microservice.IdentityModule.Domain;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
/// EventItem delete
/// </summary>
public sealed class DeleteEventItem
{
    public record Request(Guid Id) : IRequest<OperationResult<EventItemViewModel>>;

    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, OperationResult<EventItemViewModel>>
    {
        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<OperationResult<EventItemViewModel>> Handle(Request request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<EventItemViewModel>();
            var repository = unitOfWork.GetRepository<EventItem>();
            var entity = await repository.FindAsync(request.Id);
            if (entity == null)
            {
                operation.AddError(new MicroserviceNotFoundException("Entity not found"));
                return operation;
            }

            repository.Delete(entity);
            await unitOfWork.SaveChangesAsync();
            if (unitOfWork.LastSaveChangesResult.IsOk)
            {
                operation.Result = mapper.Map<EventItemViewModel>(entity);
                return operation;
            }

            operation.AddError(unitOfWork.LastSaveChangesResult.Exception);
            return operation;
        }
    }
}
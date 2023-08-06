using AutoMapper;
using Calabonga.Microservice.IdentityModule.Domain;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemsMessages.ViewModels;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemsMessages.Queries;

/// <summary>
/// EventItem by Identifier
/// </summary>
public class GetEventItemById
{
    public record Request(Guid Id) : IRequest<OperationResult<EventItemViewModel>>;

    public class Handler : IRequestHandler<Request, OperationResult<EventItemViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>Handles a request getting log by identifier</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<OperationResult<EventItemViewModel>> Handle(Request request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            var operation = OperationResult.CreateResult<EventItemViewModel>();
            var repository = _unitOfWork.GetRepository<EventItem>();
            var entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (entityWithoutIncludes == null)
            {
                operation.AddError(new MicroserviceNotFoundException($"Entity with identifier {id} not found"));
                return operation;
            }

            operation.Result = _mapper.Map<EventItemViewModel>(entityWithoutIncludes);
            return operation;
        }
    }
}
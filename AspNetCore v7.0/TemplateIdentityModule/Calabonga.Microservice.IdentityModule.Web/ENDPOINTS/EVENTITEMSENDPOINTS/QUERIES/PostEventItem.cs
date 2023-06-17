using AutoMapper;
using $ext_projectname$.Domain;
using $safeprojectname$.Endpoints.EventItemsEndpoints.ViewModels;
using Calabonga.Microservices.Core;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace $safeprojectname$.Endpoints.EventItemsEndpoints.Queries;

/// <summary>
/// Request: EventItem creation
/// </summary>
public class PostEventItem
{
    public record Request(EventItemCreateViewModel Model) : IRequest<OperationResult<EventItemViewModel>>;

    public class Handler : IRequestHandler<Request, OperationResult<EventItemViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<Handler> _logger;


        public Handler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Handler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult<EventItemViewModel>> Handle(Request eventItemRequest, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Creating new EventItem");

            var operation = OperationResult.CreateResult<EventItemViewModel>();

            var entity = _mapper.Map<EventItemCreateViewModel, EventItem>(eventItemRequest.Model);
            if (entity == null)
            {
                var exceptionMapper = new MicroserviceUnauthorizedException(AppContracts.Exceptions.MappingException);
                operation.AddError(exceptionMapper);
                _logger.LogError(exceptionMapper, "Mapper not configured correctly or something went wrong");
                return operation;
            }

            await _unitOfWork.GetRepository<EventItem>().InsertAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var lastResult = _unitOfWork.LastSaveChangesResult;
            if (lastResult.IsOk)
            {
                var mapped = _mapper.Map<EventItem, EventItemViewModel>(entity);
                operation.Result = mapped;
                operation.AddSuccess("Successfully created");
                _logger.LogInformation("New entity {@EventItem} successfully created", entity);
                return operation;
            }

            var exception = lastResult.Exception ?? new ApplicationException("Something went wrong");
            operation.AddError(exception);
            _logger.LogError(exception, "Error data saving to Database or something went wrong");

            return operation;
        }
    }
}
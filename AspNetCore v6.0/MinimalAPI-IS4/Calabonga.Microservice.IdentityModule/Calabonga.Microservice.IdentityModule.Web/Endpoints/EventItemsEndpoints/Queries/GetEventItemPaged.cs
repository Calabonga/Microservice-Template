using AutoMapper;
using Calabonga.Microservice.IdentityModule.Domain;
using Calabonga.Microservice.IdentityModule.Web.Application;
using Calabonga.Microservice.IdentityModule.Web.Endpoints.EventItemsEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.PredicatesBuilder;
using Calabonga.UnitOfWork;
using MediatR;
using System.Linq.Expressions;
using System.Reflection;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints.EventItemsEndpoints.Queries
{
    /// <summary>
    /// Request for paged list of EventItems
    /// </summary>
    public record GetEventItemPagedRequest(int PageIndex, int PageSize, string? Search) : IRequest<OperationResult<IPagedList<EventItemViewModel>>>;

    /// <summary>
    /// Request for paged list of EventItems
    /// </summary>
    public class GetEventItemPagedRequestHandler : IRequestHandler<GetEventItemPagedRequest, OperationResult<IPagedList<EventItemViewModel>>>
    {
        private readonly ILogger<GetEventItemPagedRequest> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventItemPagedRequestHandler(
            ILogger<GetEventItemPagedRequest> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<IPagedList<EventItemViewModel>>> Handle(GetEventItemPagedRequest request, CancellationToken cancellationToken)
        {
            using var _ = _logger.ShowMillisecondsElapsed(nameof(GetEventItemPagedRequestHandler));
            var operation = OperationResult.CreateResult<IPagedList<EventItemViewModel>>();
            var predicate = GetPredicate(request.Search);
            var pagedList = await _unitOfWork.GetRepository<EventItem>()
                .GetPagedListAsync(
                    predicate: predicate,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    cancellationToken: cancellationToken);

            if (pagedList == null)
            {
                operation.Result = PagedList.Empty<EventItemViewModel>();
                operation.AddWarning("Response does not return the result for pagination.");
                return operation;
            }

            if (pagedList.PageIndex > pagedList.TotalPages)
            {
                pagedList = await _unitOfWork.GetRepository<EventItem>()
                    .GetPagedListAsync(
                        pageIndex: 0,
                        pageSize: request.PageSize, cancellationToken: cancellationToken);
            }

            operation.Result = _mapper.Map<IPagedList<EventItemViewModel>>(pagedList);
            return operation;
        }

        private Expression<Func<EventItem, bool>> GetPredicate(string? search)
        {
            var predicate = PredicateBuilder.True<EventItem>();
            if (search is null)
            {
                return predicate;
            }

            predicate = predicate.And(x => x.Message.Contains(search));
            predicate = predicate.Or(x => x.Logger.Contains(search));
            predicate = predicate.Or(x => x.Level.Contains(search));
            return predicate;
        }
    }
}
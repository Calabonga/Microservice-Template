using Calabonga.Microservice.IdentityModule.Domain;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.OperationResults;
using Calabonga.PagedListCore;
using Calabonga.PredicatesBuilder;
using Calabonga.UnitOfWork;
using Mediator;
using System.Linq.Expressions;
using PredicateBuilder = Calabonga.PredicatesBuilder.PredicateBuilder;

namespace Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
/// Request for paged list of EventItems
/// </summary>
public static class GetEventItemPaged
{
    public record Request(int PageIndex, int PageSize, string? Search) : IRequest<Operation<IPagedList<EventItemViewModel>, string>>;

    public class Handler(IUnitOfWork unitOfWork)
        : IRequestHandler<Request, Operation<IPagedList<EventItemViewModel>, string>>
    {
        public async ValueTask<Operation<IPagedList<EventItemViewModel>, string>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            var predicate = GetPredicate(request.Search);
            var pagedList = await unitOfWork.GetRepository<EventItem>()
                .GetPagedListAsync(
                    predicate: predicate,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    trackingType: TrackingType.NoTracking,
                    cancellationToken: cancellationToken);

            if (pagedList.PageIndex > pagedList.TotalPages)
            {
                pagedList = await unitOfWork.GetRepository<EventItem>()
                    .GetPagedListAsync(
                        pageIndex: 0,
                        pageSize: request.PageSize,
                        trackingType: TrackingType.NoTracking,
                        cancellationToken: cancellationToken);
            }

            var mapped = PagedList.From(pagedList, items => items.Select(item => item.MapToViewModel()!));
            return Operation.Result(mapped);
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

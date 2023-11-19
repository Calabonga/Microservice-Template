using AutoMapper;
using Calabonga.Microservice.IdentityModule.Domain;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.OperationResults;
using Calabonga.PredicatesBuilder;
using Calabonga.UnitOfWork;
using MediatR;
using System.Linq.Expressions;

namespace Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
/// Request for paged list of EventItems
/// </summary>
public sealed class GetEventItemPaged
{
    public record Request(int PageIndex, int PageSize, string? Search) : IRequest<OperationResult<IPagedList<EventItemViewModel>>>;

    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, OperationResult<IPagedList<EventItemViewModel>>>
    {
        public async Task<OperationResult<IPagedList<EventItemViewModel>>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<IPagedList<EventItemViewModel>>();
            var predicate = GetPredicate(request.Search);
            var pagedList = await unitOfWork.GetRepository<EventItem>()
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
                pagedList = await unitOfWork.GetRepository<EventItem>()
                    .GetPagedListAsync(
                        pageIndex: 0,
                        pageSize: request.PageSize, cancellationToken: cancellationToken);
            }

            operation.Result = mapper.Map<IPagedList<EventItemViewModel>>(pagedList);
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
using AutoMapper;
using Calabonga.Microservice.IdentityModule.Domain;
using Calabonga.Microservice.IdentityModule.Domain.Base;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.ViewModels;
using Calabonga.PagedListCore;
using Calabonga.PredicatesBuilder;
using Calabonga.Results;
using Calabonga.UnitOfWork;
using MediatR;
using System.Linq.Expressions;

namespace Calabonga.Microservice.IdentityModule.Web.Application.Messaging.EventItemMessages.Queries;

/// <summary>
/// Request for paged list of EventItems
/// </summary>
public sealed class GetEventItemPaged
{
    public record Request(int PageIndex, int PageSize, string? Search) : IRequest<Operation<IPagedList<EventItemViewModel>, string>>;

    public class Handler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<Request, Operation<IPagedList<EventItemViewModel>, string>>
    {
        public async Task<Operation<IPagedList<EventItemViewModel>, string>> Handle(
            Request request,
            CancellationToken cancellationToken)
        {
            var predicate = GetPredicate(request.Search);
            var pagedList = await unitOfWork.GetRepository<EventItem>()
                .GetPagedListAsync(
                    predicate: predicate,
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    cancellationToken: cancellationToken);

            if (pagedList.PageIndex > pagedList.TotalPages)
            {
                pagedList = await unitOfWork.GetRepository<EventItem>()
                    .GetPagedListAsync(
                        pageIndex: 0,
                        pageSize: request.PageSize, cancellationToken: cancellationToken);
            }

            var mapped = mapper.Map<IPagedList<EventItemViewModel>>(pagedList);
            if (mapped is not null)
            {
                return Operation.Result(mapped);
            }

            return Operation.Error(AppData.Exceptions.MappingException);
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
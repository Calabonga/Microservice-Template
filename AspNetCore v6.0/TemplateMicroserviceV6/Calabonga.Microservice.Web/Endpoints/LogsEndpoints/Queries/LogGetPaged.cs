using AutoMapper;
using $ext_projectname$.Domain;
using $safeprojectname$.Endpoints.LogsEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.PredicatesBuilder;
using Calabonga.UnitOfWork;
using MediatR;
using System.Linq.Expressions;

namespace $safeprojectname$.Endpoints.LogsEndpoints.Queries;

/// <summary>
/// Request for paged list of Logs
/// </summary>
public record LogGetPagedRequest(int PageIndex, int PageSize, string? Search) : IRequest<OperationResult<IPagedList<LogViewModel>>>;

/// <summary>
/// Request for paged list of Logs
/// </summary>
public class LogGetPagedRequestHandler : IRequestHandler<LogGetPagedRequest, OperationResult<IPagedList<LogViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LogGetPagedRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationResult<IPagedList<LogViewModel>>> Handle(LogGetPagedRequest request,
        CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<IPagedList<LogViewModel>>();
        var predicate = GetPredicate(request.Search);
        var pagedList = await _unitOfWork.GetRepository<Log>()
            .GetPagedListAsync(
                predicate: predicate,
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken);

        if (pagedList == null)
        {
            operation.Result = PagedList.Empty<LogViewModel>();
            operation.AddWarning("Response does not return the result for pagination.");
            return operation;
        }

        if (pagedList.PageIndex > pagedList.TotalPages)
        {
            pagedList = await _unitOfWork.GetRepository<Log>()
                .GetPagedListAsync(
                    pageIndex: 0,
                    pageSize: request.PageSize, cancellationToken: cancellationToken);
        }

        operation.Result = _mapper.Map<IPagedList<LogViewModel>>(pagedList);
        return operation;
    }

    private Expression<Func<Log, bool>> GetPredicate(string? search)
    {
        var predicate = PredicateBuilder.True<Log>();
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
using AutoMapper;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.Microservice.IdentityModule.Web.Mediator.Base;
using Calabonga.Microservice.IdentityModule.Web.Mediator.Queries;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.Microservices.Core.Validators;
using Calabonga.OperationResults;
using Calabonga.PredicatesBuilder;
using Calabonga.UnitOfWork;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Handlers
{
    /// <summary>
    /// GetByIdentifier Handler
    /// </summary>
    public abstract class GetPagedHandlerBase<TRequest, TEntity, TQueryParams, TResponse>
        : PagedIncludesPropertyRequestHandler<TRequest, TEntity, TQueryParams, TResponse>
        where TEntity : Identity
        where TResponse : ViewModelBase
        where TQueryParams : PagedListQueryParams
        where TRequest : IRequest<OperationResult<IPagedList<TResponse>>>
    {
        #region Fields
        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderBy;
        #endregion

        protected GetPagedHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            UnitOfWork = unitOfWork;
            CurrentMapper = mapper;
        }

        #region properties

        /// <summary>
        /// CurrentMapper instance
        /// </summary>
        protected IMapper CurrentMapper { get; }

        /// <summary>
        /// Current instance of UnitOfWork
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; }

        #endregion

        /// <summary>
        /// Validate current queryParams before any data manipulations
        /// </summary>
        /// <param name="queryParams"></param>
        public virtual PermissionValidationResult ValidateQueryParams(TQueryParams queryParams) => new PermissionValidationResult();

        /// <summary>
        /// Disable include operations for current query
        /// </summary>
        public virtual bool DisableDefaultIncludes { get; } = false;

        /// <summary>
        /// Disable ChangeTracking for EntityFramework Core
        /// </summary>
        public virtual bool DisableChangesTracking { get; } = false;

        /// <summary>
        /// Overrides property name that will used for ordering operation
        /// </summary>
        /// <returns></returns>
        public virtual string GetPropertyNameForOrderBy() => string.Empty;

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public override async Task<OperationResult<IPagedList<TResponse>>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var query = request as GetPagedQuery<TResponse, TQueryParams>;
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var operation = OperationResult.CreateResult<IPagedList<TResponse>>();
            var validationResult = ValidateQueryParams(query.QueryParams);
            if (!validationResult.IsOk)
            {
                operation.AddError(validationResult.ToString()).AddData(validationResult.Errors);
                return OperationResultBeforeReturn(operation);
            }

            var properName = GetPropertyNameForOrderBy();
            if (!string.IsNullOrEmpty(properName))
            {
                _orderBy = GetOrderBy(properName, query.QueryParams.SortDirection.ToString());
            }

            var includes = DisableDefaultIncludes ? null : DefaultIncludes();
            var predicate = PredicateBuilder.True<TEntity>();
            predicate = FilterItems(predicate, query.QueryParams);
            var pagedList = await UnitOfWork.GetRepository<TEntity>()
                .GetPagedListAsync(
                    predicate: predicate,
                    orderBy: _orderBy,
                    include: includes,
                    pageIndex: query.QueryParams.PageIndex,
                    pageSize: query.QueryParams.PageSize,
                    disableTracking: DisableChangesTracking,
                    cancellationToken: cancellationToken);

            if (pagedList == null)
            {
                operation.Result = PagedList.Empty<TResponse>();
                operation.AddWarning("'Repository.GetPagedList' does not return the result for pagination.");
                return OperationResultBeforeReturn(operation);
            }

            if (pagedList.PageIndex >= pagedList.TotalPages)
            {
                pagedList = await UnitOfWork.GetRepository<TEntity>()
                    .GetPagedListAsync(
                        predicate: predicate,
                        orderBy: _orderBy,
                        include: includes,
                        pageIndex: 0,
                        pageSize: query.QueryParams.PageSize, cancellationToken: cancellationToken);
            }

            var result = ProcessOperationResult(operation, CurrentMapper.Map<IPagedList<TResponse>>(pagedList));
            if (result != null)
            {
                return OperationResultBeforeReturn(result);
            }

            var mapped = CurrentMapper.Map<IPagedList<TResponse>>(pagedList);
            operation.Result = mapped;
            return OperationResultBeforeReturn(operation);
        }

        private static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetOrderBy(string orderColumn, string orderType)
        {
            var typeQueryable = typeof(IQueryable<TEntity>);
            var argQueryable = Expression.Parameter(typeQueryable, "p");
            var outerExpression = Expression.Lambda(argQueryable, argQueryable);
            var props = orderColumn.Split('.');
            var type = typeof(TEntity);
            var arg = Expression.Parameter(type, "x");

            Expression expr = arg;
            foreach (var prop in props)
            {
                var pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (pi == null)
                {
                    continue;
                }

                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            var lambda = Expression.Lambda(expr, arg);
            var methodName = orderType == "Ascending" ? "OrderBy" : "OrderByDescending";

            var resultExp = Expression.Call(typeof(Queryable), methodName, new[] { typeof(TEntity), type },
                outerExpression.Body, Expression.Quote(lambda));
            var finalLambda = Expression.Lambda(resultExp, argQueryable);
            return (Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>)finalLambda.Compile();
        }
    }
}
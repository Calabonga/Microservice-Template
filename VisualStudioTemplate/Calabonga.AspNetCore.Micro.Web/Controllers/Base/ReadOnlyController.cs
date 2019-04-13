using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.AspNetCore.Micro.Core;
using Calabonga.AspNetCore.Micro.Core.Exceptions;
using Calabonga.AspNetCore.Micro.Data;
using Calabonga.AspNetCore.Micro.Models.Base;
using $safeprojectname$.Infrastructure.QueryParams;
using $safeprojectname$.Infrastructure.Services;
using $safeprojectname$.Infrastructure.Settings;
using $safeprojectname$.Infrastructure.Validations.Base;
using $safeprojectname$.Infrastructure.ViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.OperationResultsCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;

namespace $safeprojectname$.Controllers.Base
{
    /// <summary>
    /// Represent read only operations controller for entity. It's not required special ViewModels and other things
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TQueryParams"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    public abstract class ReadOnlyController<TEntity, TViewModel, TQueryParams> : UnitOfWorkController
        where TEntity : Identity
        where TViewModel : ViewModelBase
        where TQueryParams : PagedListQueryParams
    {
        #region Fields

        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderBy;

        #endregion

        /// <inheritdoc />
        protected ReadOnlyController(
            IMapper mapper,
            IOptions<CurrentAppSettings> options,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork,
            IAccountService accountService) : base(options, unitOfWork, accountService)
        {
            CurrentMapper = mapper;
            Repository = unitOfWork.GetRepository<TEntity>();
        }

        /// <summary>
        /// Active AutoMapper instance
        /// </summary>
        protected IMapper CurrentMapper { get; }

        /// <summary>
        /// Current Entity repository
        /// </summary>
        protected IRepository<TEntity> Repository { get; }

        /// <summary>
        /// If you need to include something you need override this method
        /// </summary>
        /// <returns></returns>
        protected virtual Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> GetIncludes()
        {
            return null;
        }

        #region GetAsync

        /// <summary>
        /// Returns entity from repository by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<OperationResult<TViewModel>>> GetAsync(Guid id)
        {
            var includes = GetIncludes();
            var entity = Repository.GetFirstOrDefault(predicate: x => x.Id == id, include: includes);
            if (entity == null)
            {
                return OperationResultError<TViewModel>(null, AppData.Exceptions.NotFoundException);
            }

            var accessRights = ValidateUserRolesAndRights(entity);
            if (!accessRights.IsOk)
            {
                var adminRights = await AccountService.IsInRolesAsync(new[] {AppData.SystemAdministratorRoleName});
                if (adminRights == null || !adminRights.IsOk)
                {
                    return OperationResultError<TViewModel>(null, accessRights.ToString());
                }
            }

            var mapped = CurrentMapper.Map<TEntity, TViewModel>(entity);
            return OperationResultSuccess(mapped);
        }

        #endregion

        #region GetPaged

        /// <summary>
        /// Summary description will be replaced by IOperationFilter (swagger)
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        /// <response code="404">Unauthorized access detected or parameters are not valid</response>
        [HttpGet("paged")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OperationResult<IPagedList<TViewModel>>>> GetPaged([FromQuery] TQueryParams queryParams)
        {
            var accessRights = ValidateQueryParams(queryParams);
            if (!accessRights.IsOk)
            {
                var adminRights = await AccountService.IsInRolesAsync(new[] {AppData.SystemAdministratorRoleName});
                if (adminRights == null || !adminRights.IsOk)
                {
                    return OperationResultError<IPagedList<TViewModel>>(null, accessRights.ToString());
                }
            }

            ApplyDefaultSettings(queryParams);
            var properName = OrderByPropertyName();
            if (!string.IsNullOrEmpty(properName))
            {
                _orderBy = GetOrderBy(properName, queryParams.SortDirection.ToString());
            }

            var includes = GetIncludes();
            var predicate = PredicateBuilder.True<TEntity>();
            predicate = FilterItems(predicate, queryParams);
            var items = Repository.GetPagedList(predicate: predicate, orderBy: _orderBy, include: includes, pageIndex: queryParams.PageIndex, pageSize: queryParams.PageSize);
            var mapped = CurrentMapper.Map<IPagedList<TViewModel>>(items);
            return OperationResultSuccess(mapped, $"Total count: {items.TotalCount}");
        }

        #endregion

        #region Abstract and Virtual

        private static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetOrderBy(string orderColumn,
            string orderType)
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
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            var lambda = Expression.Lambda(expr, arg);
            var methodName = orderType == "Ascending" ? "OrderBy" : "OrderByDescending";

            var resultExp = Expression.Call(typeof(Queryable), methodName, new[] {typeof(TEntity), type},
                outerExpression.Body, Expression.Quote(lambda));
            var finalLambda = Expression.Lambda(resultExp, argQueryable);
            return (Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>) finalLambda.Compile();
        }

        /// <summary>
        /// Return default predicate for filtering PagedList result
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, bool>> FilterItems(Expression<Func<TEntity, bool>> predicate, TQueryParams queryParams)
        {
            return null;
        }

        #endregion

        /// <summary>
        /// PropertyName for ordering by for enable pagination. You can override default behavior.
        /// </summary>
        /// <returns></returns>
        protected virtual string OrderByPropertyName()
        {
            return string.Empty;
        }

        /// <summary>
        /// Validate current user roles and access rights
        /// </summary>
        /// <param name="queryParams"></param>
        protected virtual PermissionValidationResult ValidateQueryParams(TQueryParams queryParams)
        {
            return new PermissionValidationResult();
        }

        /// <summary>
        /// Validate current user roles and access rights
        /// </summary>
        /// <param name="entity"></param>
        protected virtual PermissionValidationResult ValidateUserRolesAndRights(TEntity entity)
        {
            return new PermissionValidationResult();
        }

        /// <summary>
        /// Ensure user identifier is equals authorized user id
        /// </summary>
        /// <param name="requestedUserId"></param>
        /// <returns></returns>
        protected PermissionValidationResult EnsureUserCanAccess(Guid requestedUserId)
        {
            if (AccountService == null)
            {
                throw new MicroserviceArgumentNullException();
            }

            var userId = AccountService.GetCurrentUserId();
            return requestedUserId != userId
                ? new UnauthorizedPermissionValidationResult()
                : new PermissionValidationResult();
        }

        /// <summary>
        /// Use CurrentAppSettings to apply some parameters when they are empty
        /// </summary>
        /// <param name="queryParams"></param>
        private void ApplyDefaultSettings(TQueryParams queryParams)
        {
            if (queryParams.PageSize < 1)
            {
                queryParams.PageSize = CurrentSettings.DefaultPageSize;
            }
        }
    }
}
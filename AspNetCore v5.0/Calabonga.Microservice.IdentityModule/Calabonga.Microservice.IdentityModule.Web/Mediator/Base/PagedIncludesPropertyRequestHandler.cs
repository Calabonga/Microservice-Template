using AutoMapper;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Base
{
    /// <summary>
    /// GetByIdentifier Handler
    /// </summary>
    public abstract class PagedIncludesPropertyRequestHandler<TRequest, TEntity,TQueryParams, TResponse> 
        : PagedOperationResultRequestHandlerBase<TRequest, TResponse>, IHasNavigationProperties<TEntity>
        where TRequest : IRequest<OperationResult<IPagedList<TResponse>>>
        where TResponse : ViewModelBase
        where TQueryParams : PagedListQueryParams
        where TEntity : Identity
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        protected PagedIncludesPropertyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns Includes for entity
        /// </summary>
        /// <returns></returns>
        public virtual Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> DefaultIncludes() => null;

        /// <summary>
        /// Returns predicate for filtering before processing items
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public virtual Expression<Func<TEntity, bool>> FilterItems(Expression<Func<TEntity, bool>> predicate, TQueryParams queryParams) => predicate;

        /// <summary>
        /// Process operation result
        /// </summary>
        /// <param name="operationResult"></param>
        /// <returns></returns>
        public virtual OperationResult<IPagedList<TResponse>> OperationResultBeforeReturn(OperationResult<IPagedList<TResponse>> operationResult) => operationResult;
    }
}
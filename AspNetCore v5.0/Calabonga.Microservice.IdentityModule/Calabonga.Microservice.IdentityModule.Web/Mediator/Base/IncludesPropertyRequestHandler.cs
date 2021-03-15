using AutoMapper;
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.EntityFrameworkCore.Entities.Base;
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
    public abstract class IncludesPropertyRequestHandler<TRequest, TEntity, TResponse>
        : OperationResultRequestHandlerBase<TRequest, TResponse>, IHasNavigationProperties<TEntity>
        where TRequest : IRequest<OperationResult<TResponse>>
        where TResponse : ViewModelBase
        where TEntity : Identity
    {

        protected IncludesPropertyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            CurrentMapper = mapper;
        }

        /// <summary>
        /// Current instance of IMapper
        /// </summary>
        protected IMapper CurrentMapper { get; }

        /// <summary>
        /// Current instance of IUnitOfWork
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Returns Includes for entity
        /// </summary>
        /// <returns></returns>
        public virtual Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> DefaultIncludes() => null;

        /// <summary>
        /// Returns predicate for filtering before processing items
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, bool>> FilterItems(Expression<Func<TEntity, bool>> predicate) => predicate;

        /// <summary>
        /// Process operation result
        /// </summary>
        /// <param name="operationResult"></param>
        /// <returns></returns>
        protected virtual OperationResult<TResponse> OperationResultBeforeReturn(OperationResult<TResponse> operationResult)
        {
            return operationResult;
        }

        /// <summary>
        /// Process operation result
        /// </summary>
        /// <param name="operationResult"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected virtual OperationResult<TResponse> ProcessOperationResult(OperationResult<TResponse> operationResult, TResponse response)
        {
            return null;
        }
    }
}
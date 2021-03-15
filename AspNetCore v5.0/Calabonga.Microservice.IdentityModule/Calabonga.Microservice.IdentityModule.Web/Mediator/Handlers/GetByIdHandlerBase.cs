using AutoMapper;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.Microservice.IdentityModule.Web.Mediator.Base;
using Calabonga.Microservice.IdentityModule.Web.Mediator.Queries;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.PredicatesBuilder;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Handlers
{
    /// <summary>
    /// GetByIdentifier Handler
    /// </summary>
    public abstract class GetByIdHandlerBase<TRequest, TEntity, TResponse> : IncludesPropertyRequestHandler<TRequest, TEntity, TResponse>
        where TRequest : IRequest<OperationResult<TResponse>>
        where TResponse : ViewModelBase
        where TEntity : Identity
    {

        protected GetByIdHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public override async Task<OperationResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var id = (request as GetByIdQuery<TResponse>)?.Id;
            var operation = OperationResult.CreateResult<TResponse>();
            var repository = UnitOfWork.GetRepository<TEntity>();
            var predicate = PredicateBuilder.True<TEntity>();
            var includes = DefaultIncludes();
            var filterItems = FilterItems(predicate);
            if (filterItems == null)
            {
                var entityWithoutIncludes = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == id, include: includes);
                if (entityWithoutIncludes == null)
                {
                    operation.AddError(new MicroserviceNotFoundException($"Entity with identifier {id} not found"));
                    return operation;
                }
                operation.Result = CurrentMapper.Map<TResponse>(entityWithoutIncludes);
                return OperationResultBeforeReturn(operation);
            }

            var items = repository.GetAll(true).Where(filterItems);
            if (includes != null && await items.AnyAsync(cancellationToken))
            {
                items = includes(items);
            }

            var entity = await items.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            var result = ProcessOperationResult(operation, CurrentMapper.Map<TResponse>(entity));
            if (result != null)
            {
                return OperationResultBeforeReturn(result);
            }

            operation.Result = CurrentMapper.Map<TResponse>(entity);
            return OperationResultBeforeReturn(operation);
        }
    }
}
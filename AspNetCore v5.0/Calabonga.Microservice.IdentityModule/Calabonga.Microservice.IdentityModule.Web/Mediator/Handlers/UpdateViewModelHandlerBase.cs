using AutoMapper;
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.Microservice.IdentityModule.Web.Mediator.Queries;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Handlers
{
    /// <summary>
    /// Implemented query to DB with mapping
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TUpdateViewModel"></typeparam>
    public abstract class UpdateViewModelHandlerBase<TQuery, TEntity, TUpdateViewModel> : OperationResultRequestHandlerBase<TQuery, TUpdateViewModel>
        where TQuery : IRequest<OperationResult<TUpdateViewModel>>
        where TUpdateViewModel : ViewModelBase, new()
        where TEntity : Identity
    {
       
        protected UpdateViewModelHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
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

        public override async Task<OperationResult<TUpdateViewModel>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<TUpdateViewModel>();
            var entityId = (request as UpdateViewModelQuery<TUpdateViewModel>)?.Id;
            if (entityId.HasValue && entityId.Value != Guid.Empty)
            {
                var entity = await UnitOfWork.GetRepository<TEntity>().GetFirstOrDefaultAsync(predicate: x => x.Id == entityId);
                if (entity == null)
                {
                    operation.AddError(new MicroserviceNotFoundException($"{entityId}"));
                    return operation;
                }

                operation.Result = CurrentMapper.Map<TUpdateViewModel>(entity);
                return operation;
            }
            operation.AddError(new MicroserviceNotFoundException($"{entityId}"));
            return operation;
        }
    }
}
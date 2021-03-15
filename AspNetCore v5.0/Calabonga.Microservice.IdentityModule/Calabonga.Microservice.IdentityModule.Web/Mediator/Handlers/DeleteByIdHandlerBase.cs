using AutoMapper;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.Microservice.IdentityModule.Web.Mediator.Queries;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Handlers
{
    /// <summary>
    /// Mediator Handler for Deleting entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    public abstract class DeleteByIdHandlerBase<TEntity, TViewModel> : IRequestHandler<DeleteByIdQuery<TEntity, TViewModel>, OperationResult<TViewModel>>
        where TEntity : Identity
        where TViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        protected DeleteByIdHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        protected virtual OperationResult<TViewModel> OperationResultBeforeReturn(OperationResult<TViewModel> operationResult) => operationResult;

        protected virtual bool IsAutoHistoryEnabled { get; } = false;

        public async Task<OperationResult<TViewModel>> Handle(DeleteByIdQuery<TEntity, TViewModel> request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<TViewModel>();
            var repository = _unitOfWork.GetRepository<TEntity>();
            var entity = repository.Find(request.Id);
            if (entity == null)
            {
                operation.AddError(new MicroserviceNotFoundException("Entity not found"));
                return OperationResultBeforeReturn(operation);
            }
            repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync(IsAutoHistoryEnabled);
            if (_unitOfWork.LastSaveChangesResult.IsOk)
            {
                var result = ProcessOperationResult(operation, _mapper.Map<TEntity, TViewModel>(entity));
                if (result != null)
                {
                    return OperationResultBeforeReturn(result);
                }
                operation.Result = _mapper.Map<TEntity, TViewModel>(entity);
                return OperationResultBeforeReturn(operation);
            }
            operation.AddError(_unitOfWork.LastSaveChangesResult.Exception);
            return OperationResultBeforeReturn(operation);
        }

        /// <summary>
        /// Process operation result
        /// </summary>
        /// <param name="operationResult"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected virtual OperationResult<TViewModel> ProcessOperationResult(OperationResult<TViewModel> operationResult, TViewModel response) => null;
    }
}
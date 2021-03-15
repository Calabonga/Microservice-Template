using AutoMapper;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.Microservice.IdentityModule.Web.Mediator.Base;
using Calabonga.Microservice.IdentityModule.Web.Mediator.CreateSteps;
using Calabonga.Microservice.IdentityModule.Web.Mediator.Queries;
using Calabonga.Microservice.IdentityModule.Web.Mediator.UpdateSteps;
using Calabonga.Microservices.Core;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Handlers
{
    /// <summary>
    /// GetByIdentifier Handler
    /// </summary>
    public abstract class PutItemHandlerBase<TEntity, TViewModel, TUpdateViewModel>
        : IRequestHandler<PutItemQuery<TEntity, TViewModel, TUpdateViewModel>, OperationResult<TViewModel>>
        where TEntity : Identity
        where TViewModel : ViewModelBase
        where TUpdateViewModel : ViewModelBase, new()
    {
        #region private fields

        #endregion

        protected PutItemHandlerBase(
            IUnitOfWork unitOfWork,
            IMapper currentMapper)
        {
            UnitOfWork = unitOfWork;
            CurrentMapper = currentMapper;
            Context = new HandlerContext<TEntity, TViewModel>(unitOfWork);
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

        /// <summary>
        /// Current command context
        /// </summary>
        private HandlerContext<TEntity, TViewModel> Context { get; }

        /// <summary>
        /// Enable/Disable AutoHistory mechanism for update logging changes in special data table. Default: false
        /// </summary>
        public virtual bool IsAutoHistoryEnabled { get; } = false;

        /// <summary>
        /// Wrapper for operation result post process manipulations
        /// </summary>
        /// <param name="result"></param>
        /// <param name="operationResult"></param>
        /// <returns></returns>
        public virtual OperationResult<TViewModel> OperationResultBeforeReturn(HandlerStep result, OperationResult<TViewModel> operationResult) => operationResult;

        /// <summary>
        /// Setting up user audit information for operation
        /// </summary>
        /// <param name="entity"></param>
        public virtual void SetAuditInformation(TEntity entity)
        {

        }

        #endregion

        #region OnEdit Handlers

        /// <summary>
        /// Fires after SaveChanges failed
        /// </summary>
        /// <param name="step"></param>
        public virtual Task<HandlerStep> OnUpdateAfterSaveChangesFailedAsync(HandlerStep step) => Task.FromResult(step);

        /// <summary>
        /// Fires when validation already complete and next step is saving entity on update operations
        /// </summary>
        /// <param name="step"></param>
        public virtual Task<HandlerStep> OnUpdateBeforeSaveChangesAsync(HandlerStep step) => Task.FromResult(step);

        /// <summary>
        /// Fires before entity validation executed on entity updating
        /// </summary>
        /// <param name="step"></param>
        public virtual Task<HandlerStep> OnUpdateBeforeAnyValidationsAsync(HandlerStep step) => Task.FromResult(step);

        /// <summary>
        /// Fires before any mapping operations begin
        /// </summary>
        /// <param name="step"></param>
        public virtual Task<HandlerStep> OnUpdateBeforeMappingsAsync(HandlerStep step) => Task.FromResult(step);

        /// <summary>
        /// Fires after changes successfully saved
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public virtual Task<HandlerStep> OnUpdateAfterSaveChangesSuccessAsync(HandlerStep step) => Task.FromResult(step);

        #endregion


        public async Task<OperationResult<TViewModel>> Handle(PutItemQuery<TEntity, TViewModel, TUpdateViewModel> request, CancellationToken cancellationToken)
        {
            Context.InitOrUpdate();

            await using var transaction = await UnitOfWork.BeginTransactionAsync();
            Context.AddOrUpdateUpdateViewModel(request.UpdateViewModel);

            // step 1: Find entity
            var step1 = new EmptyStep();
            var entity = await FindEntity(request.UpdateViewModel);
            if (entity == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                Context.AddError(new MicroserviceUnauthorizedException(AppContracts.Exceptions.NotFoundException));
                return OperationResultBeforeReturn(step1, Context.GetOperationResult());
            }

            Context.AddOrUpdateEntity(entity);

            // step 1: Set audit information
            var step2 = new OnUpdateBeforeMappingStep(Context);
            SetAuditInformation(entity);

            // step 3
            await OnUpdateBeforeMappingsAsync(step2);
            if (step2.IsStopped)
            {
                await transaction.RollbackAsync(cancellationToken);
                return OperationResultBeforeReturn(step2, Context.GetOperationResult());
            }

            // step 4: Mapping
            CurrentMapper.Map(request.UpdateViewModel, entity);

            var step3 = new OnUpdateBeforeAnyValidationsStep(Context);
            await OnUpdateBeforeAnyValidationsAsync(step3);
            if (step3.IsStopped)
            {
                await transaction.RollbackAsync(cancellationToken);
                return OperationResultBeforeReturn(step3, Context.GetOperationResult());
            }

            // step 3: before save changes
            var step4 = new OnUpdateBeforeSaveChangesStep(Context);
            await OnUpdateBeforeSaveChangesAsync(step4);
            if (step4.IsStopped)
            {
                await transaction.RollbackAsync(cancellationToken);
                return OperationResultBeforeReturn(step4, Context.GetOperationResult());
            }

            UnitOfWork.GetRepository<TEntity>().Update(entity);
            await UnitOfWork.SaveChangesAsync(IsAutoHistoryEnabled);

            var lastResult = UnitOfWork.LastSaveChangesResult;
            if (lastResult.IsOk)
            {
                var operationResult = Context.GetOperationResult();
                var step41 = new OnUpdateOperationResultBuildStep(Context);
                var result = ProcessOperationResult(operationResult, CurrentMapper.Map<TEntity, TViewModel>(entity));
                if (step41.IsStopped)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return OperationResultBeforeReturn(step41, Context.GetOperationResult());
                }
                if (result != null)
                {
                    return OperationResultBeforeReturn(step41, result);
                }

                var mapped = CurrentMapper.Map<TEntity, TViewModel>(entity);
                Context.SetOperationResult(mapped);
                var step5 = new OnUpdateAfterSaveChangeStep(Context);

                await OnUpdateAfterSaveChangesSuccessAsync(step5);
                if (step5.IsStopped)
                {
                    await transaction.CommitAsync(cancellationToken);
                    return OperationResultBeforeReturn(step5, Context.GetOperationResult());
                }
            }
            var step6 = new OnUpdateAfterSaveChangeStep(Context);

            var operation = Context.GetOperationResult();
            await OnUpdateAfterSaveChangesFailedAsync(step6);
            if (!step6.IsStopped)
            {
                return OperationResultBeforeReturn(step6, operation);
            }

            await transaction.RollbackAsync(cancellationToken);
            if (operation.Exception == null)
            {
                operation.AddError(lastResult.Exception);
            }
            return OperationResultBeforeReturn(step6, operation);
        }

        /// <summary>
        /// Returns entity from DbContext for editing
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindEntity(TUpdateViewModel model) => UnitOfWork.GetRepository<TEntity>().GetFirstOrDefaultAsync(predicate: x => x.Id == model.Id);

        /// <summary>
        /// Process operation result
        /// </summary>
        /// <param name="operationResult"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected virtual OperationResult<TViewModel> ProcessOperationResult(OperationResult<TViewModel> operationResult, TViewModel response) => null;
    }
}
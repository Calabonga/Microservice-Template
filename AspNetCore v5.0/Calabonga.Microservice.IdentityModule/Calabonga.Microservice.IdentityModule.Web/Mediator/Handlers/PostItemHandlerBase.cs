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
    public abstract class PostItemHandlerBase<TEntity, TViewModel, TCreateViewModel> :
        IRequestHandler<PostItemQuery<TEntity, TViewModel, TCreateViewModel>, OperationResult<TViewModel>>
        where TEntity : Identity
        where TViewModel : ViewModelBase
        where TCreateViewModel : class, IViewModel, new()
    {

        protected PostItemHandlerBase(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            CurrentMapper = mapper;
            Context = new HandlerContext<TEntity, TViewModel>(unitOfWork);
        }

        #region properties

        /// <summary>
        /// CurrentMapper instance
        /// </summary>
        protected IMapper CurrentMapper { get; }

        /// <summary>
        /// Current instance UnitOfWork
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Instance of <see cref="IHandlerContext{TEntity,TViewModel}"/> for current operation
        /// </summary>
        private HandlerContext<TEntity, TViewModel> Context { get; }

        #endregion

        #region OnCreated Handlers

        /// <summary>
        /// Fires before any mapping operation.
        /// This is first step for Create pipeline
        /// OrderIndex 1
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        protected virtual Task<HandlerStep> OnCreateBeforeMappingAsync(HandlerStep step) => Task.FromResult(step);

        /// <summary>
        /// Fires before entity validation executed on entity creation
        /// This is second step for Create pipeline
        /// OrderIndex 2
        /// </summary>
        /// <param name="step"></param>
        protected virtual Task<HandlerStep> OnCreateBeforeAnyValidationsAsync(HandlerStep step) => Task.FromResult(step);

        /// <summary>
        /// Fires when entity ready to Insert but some operations still need to do before saveChanges executed
        /// This is third step for Create pipeline
        /// OrderIndex 3
        /// </summary>
        /// <param name="step"></param>
        protected virtual Task<HandlerStep> OnCreateBeforeSaveChangesAsync(HandlerStep step) => Task.FromResult(step);

        /// <summary>
        /// Fires after changes successfully saved
        /// This is fourth step for Create pipeline
        /// OrderIndex 4
        /// </summary>
        /// <param name="step"></param>
        protected virtual Task<HandlerResult> OnCreateAfterSaveChangesSuccessAsync(HandlerResult step) => Task.FromResult(step);

        /// <summary>
        /// Fires after SaveChanges failed. In following after step 3.
        /// This is fifth step for Create pipeline.
        /// OrderIndex 5
        /// </summary>
        /// <param name="step"></param>
        protected virtual Task<HandlerResult> OnCreateAfterSaveChangesFailedAsync(HandlerResult step) => Task.FromResult(step);

        /// <summary>
        /// Wrapper for operation result post process manipulations
        /// </summary>
        /// <param name="step"></param>
        /// <param name="operationResult">
        /// </param>
        /// <returns></returns>
        protected virtual OperationResult<TViewModel> OperationResultBeforeReturn(HandlerStep step, OperationResult<TViewModel> operationResult) => operationResult;

        /// <summary>
        /// Setting up user audit information for operation
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void SetAuditInformation(TEntity entity)
        {

        }

        #endregion

        public async Task<OperationResult<TViewModel>> Handle(PostItemQuery<TEntity, TViewModel, TCreateViewModel> request, CancellationToken cancellationToken)
        {
            Context.InitOrUpdate();

            await using var transaction = await UnitOfWork.BeginTransactionAsync();
            Context.AddOrUpdateCreateViewModel(request.CreateViewModel);

            // Step 1: Mapping
            var step1 = new OnCreateBeforeMappingStep(Context);
            await OnCreateBeforeMappingAsync(step1);
            if (step1.IsStopped)
            {
                await transaction.RollbackAsync(cancellationToken);
                return OperationResultBeforeReturn(step1, Context.GetOperationResult());
            }

            var entity = CurrentMapper.Map<TCreateViewModel, TEntity>(request.CreateViewModel);
            if (entity == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                Context.AddError(new MicroserviceUnauthorizedException(AppContracts.Exceptions.MappingException));
                return OperationResultBeforeReturn(step1, Context.GetOperationResult());
            }

            // Step 2: Set audit information
            SetAuditInformation(entity);

            // Step 3: On create before validations
            Context.AddOrUpdateEntity(entity);
            var step3 = new OnCreateBeforeAnyValidationsStep(Context);
            await OnCreateBeforeAnyValidationsAsync(step3);
            if (step3.IsStopped)
            {
                await transaction.RollbackAsync(cancellationToken);
                return OperationResultBeforeReturn(step3, Context.GetOperationResult());
            }

            // Step 4: Create Before SaveChanges
            var step4 = new OnCreateBeforeSaveChangesStep(Context);
            await OnCreateBeforeSaveChangesAsync(step4);
            if (step4.IsStopped)
            {
                await transaction.RollbackAsync(cancellationToken);
                return OperationResultBeforeReturn(step4, Context.GetOperationResult());
            }

            await UnitOfWork.GetRepository<TEntity>().InsertAsync(entity, cancellationToken);
            await UnitOfWork.SaveChangesAsync();

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

                // Step 5:
                var mapped = CurrentMapper.Map<TEntity, TViewModel>(entity);
                Context.SetOperationResult(mapped);
                var step5 = new OnCreateAfterSaveChangesStep(Context);
                await OnCreateAfterSaveChangesSuccessAsync(step5);
                if (step5.IsStopped)
                {
                    await transaction.CommitAsync(cancellationToken);
                    return OperationResultBeforeReturn(step5, Context.GetOperationResult());
                }
            }
            // Step 6:
            var step6 = new OnCreateAfterSaveChangesStep(Context);

            var operation = Context.GetOperationResult();
            await OnCreateAfterSaveChangesFailedAsync(step6);
            if (step6.IsStopped)
            {
                if (operation.Exception == null)
                {
                    operation.AddError(lastResult.Exception);
                }
            }
            await transaction.RollbackAsync(cancellationToken);
            return OperationResultBeforeReturn(step6, operation);
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
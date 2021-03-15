using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using System;
using System.Collections.Generic;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Base
{
    /// <summary>
    /// Context for Handler
    /// </summary>
    public class HandlerContext<TEntity, TViewModel> : ParamsProperty, IHandlerContext<TEntity, TViewModel>
        where TEntity : Identity
    {
        private const string OperationResultKeyName = "OperationResult";
        private const string ViewModelKeyName = "ViewModel";
        private const string CreateModelKeyName = "CreateViewModel";
        private const string UpdateModelKeyName = "UpdateViewModel";
        private const string EntityKeyName = "Entity";

        public HandlerContext(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        /// <summary>
        /// UnitOfWork instance
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Initiate new instance of the OperationResult or Update if exists
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        public void InitOrUpdate(OperationResult<TViewModel> operationResult = null)
        {
            operationResult ??= OperationResult.CreateResult<TViewModel>();
            AddOrUpdateParameter(OperationResultKeyName, operationResult);
        }

        #region AddError

        /// <summary>
        /// Add object as TEntity
        /// </summary>
        /// <param name="entity"></param>
        public void AddOrUpdateEntity(object entity)
        {
            AddOrUpdateParameter(EntityKeyName, (TEntity)entity);
        }

        /// <summary>
        /// Add to OperationResult metadata message
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="exception"></param>
        /// <param name="dataObject"></param>
        public void AddError(Exception exception, object dataObject = null)
        {
            var operationResult = GetParamByName<OperationResult<TViewModel>>(OperationResultKeyName);
            if (operationResult == null)
            {
                throw new ArgumentNullException(OperationResultKeyName);
            }

            if (dataObject == null)
            {
                operationResult.AddError(exception);
            }
            else
            {
                operationResult.AddError(exception).AddData(dataObject);
            }
            InitOrUpdate(operationResult);
        }

        /// <summary>
        /// Add to OperationResult metadata message
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="dataObject"></param>
        public void AddError(string errorMessage, object dataObject = null)
        {
            var operationResult = GetParamByName<OperationResult<TViewModel>>(OperationResultKeyName);
            if (operationResult == null)
            {
                throw new ArgumentNullException(OperationResultKeyName);
            }

            if (dataObject == null)
            {
                operationResult.AddError(errorMessage);
            }
            else
            {
                operationResult.AddError(errorMessage).AddData(dataObject);
            }
            InitOrUpdate(operationResult);
        }

        #endregion

        #region AddWarning

        /// <summary>
        /// Add to OperationResult metadata message
        /// </summary>
        /// <param name="warningMessage"></param>
        /// <param name="dataObject"></param>
        public void AddWarning(string warningMessage, object dataObject = null)
        {
            var operationResult = GetParamByName<OperationResult<TViewModel>>(OperationResultKeyName);
            if (operationResult == null)
            {
                throw new ArgumentNullException(OperationResultKeyName);
            }

            if (dataObject == null)
            {
                operationResult.AddWarning(warningMessage);
            }
            else
            {
                operationResult.AddWarning(warningMessage).AddData(dataObject);
            }
            InitOrUpdate(operationResult);
        }

        #endregion

        #region AddSuccess

        /// <summary>
        /// Add to OperationResult metadata message
        /// </summary>
        /// <param name="successMessage"></param>
        /// <param name="dataObject"></param>
        public void AddSuccess(string successMessage, object dataObject = null)
        {
            var operationResult = GetParamByName<OperationResult<TViewModel>>(OperationResultKeyName);
            if (operationResult == null)
            {
                throw new ArgumentNullException(OperationResultKeyName);
            }

            if (dataObject == null)
            {
                operationResult.AddSuccess(successMessage);
            }
            else
            {
                operationResult.AddSuccess(successMessage).AddData(dataObject);
            }
            InitOrUpdate(operationResult);
        }

        #endregion

        #region AddInfo

        /// <summary>
        /// Add to OperationResult metadata message
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="infoMessage"></param>
        public void AddInfo(string infoMessage)
        {
            var operationResult = GetParamByName<OperationResult<TViewModel>>(OperationResultKeyName);
            if (operationResult == null)
            {
                throw new ArgumentNullException(OperationResultKeyName);
            }

            operationResult.AddInfo(infoMessage);
            InitOrUpdate(operationResult);
        }

        /// <summary>
        /// Add to OperationResult metadata message
        /// </summary>
        /// <param name="infoMessage"></param>
        /// <param name="dataObject"></param>
        public void AddInfo(string infoMessage, object dataObject)
        {
            var operationResult = GetParamByName<OperationResult<TViewModel>>(OperationResultKeyName);
            if (operationResult == null)
            {
                throw new ArgumentNullException(OperationResultKeyName);
            }

            operationResult.AddInfo(infoMessage).AddData(dataObject);
            InitOrUpdate(operationResult);
        }

        #endregion

        /// <summary>
        /// Add to writable context some object as parameter
        /// </summary>
        public void AddOrUpdateParameter(string name, object value)
        {
            base.AddOrUpdateParameter(name, value);
        }

        /// <summary>
        /// Returns Entity model for current controller <see cref="TEntity"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public TEntity GetEntity()
        {
            var entity = GetParamByName<TEntity>(EntityKeyName);
            if (entity == null)
            {
                throw new ArgumentNullException(EntityKeyName);
            }
            return entity;
        }

        /// <summary>
        /// Returns ViewModel for creation in current operation
        /// </summary>
        /// <returns></returns>
        public object GetCreateViewModel()
        {
            var entity = GetParamByName(CreateModelKeyName);
            if (entity == null)
            {
                throw new ArgumentNullException(EntityKeyName);
            }
            return entity;
        }

        public object GetParamByName(string objectParserResultName) => throw new NotImplementedException();


        /// <summary>
        /// Return OperationResult for current operations context
        /// </summary>
        /// <returns></returns>
        object IHandlerContext.GetOperationResult()
        {
            var operationResult = GetParamByName<OperationResult<TViewModel>>(OperationResultKeyName);
            if (operationResult == null)
            {
                throw new ArgumentNullException(OperationResultKeyName);
            }
            return GetOperationResult();
        }

        /// <summary>
        /// Creates UpdateViewModel for current controller <see cref="TCreateViewModel"/>
        /// </summary>
        /// <typeparam name="TCreateViewModel"></typeparam>
        /// <returns></returns>
        public TCreateViewModel GetCreateViewModel<TCreateViewModel>()
        {
            var createViewModel = GetParamByName<TCreateViewModel>(CreateModelKeyName);
            if (createViewModel == null)
            {
                throw new ArgumentNullException(CreateModelKeyName);
            }

            return createViewModel;
        }

        /// <summary>
        /// Returns UpdateViewModel for current controller <see cref="TUpdateViewModel"/>
        /// </summary>
        /// <typeparam name="TUpdateViewModel"></typeparam>
        /// <returns></returns>
        public TUpdateViewModel GetUpdateViewModel<TUpdateViewModel>()
        {
            var updateViewModel = GetParamByName<TUpdateViewModel>(UpdateModelKeyName);
            if (updateViewModel == null)
            {
                throw new ArgumentNullException(UpdateModelKeyName);
            }

            return updateViewModel;
        }

        /// <summary>
        /// Appends logs message into OperationResult in the WritableContext
        /// </summary>
        /// <param name="message"></param>
        public void AppendLog(string message)
        {
            var operationResult = GetOperationResult();
            operationResult.AppendLog(message);
            InitOrUpdate(operationResult);
        }

        /// <summary>
        /// Appends logs message into OperationResult in the WritableContext
        /// </summary>
        /// <param name="logs"></param>
        public void AppendLog(IEnumerable<string> logs)
        {
            var operationResult = GetOperationResult();
            operationResult.AppendLog(logs);
            InitOrUpdate(operationResult);
        }

        /// <summary>
        /// Returns OperationResult Result entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public TViewModel GetResult()
        {
            var operationResult = GetOperationResult();
            if (operationResult.Result == null)
            {
                throw new ArgumentNullException(ViewModelKeyName);
            }
            return operationResult.Result;
        }

        /// <summary>
        /// Sets the Result for OperationResult
        /// </summary>
        /// <param name="result"></param>
        public void SetOperationResult(TViewModel result)
        {
            var operationResult = GetOperationResult();
            operationResult.Result = result;
            InitOrUpdate(operationResult);
        }

        /// <summary>
        /// Returns Message for OperationResult from context of the WritableController
        /// </summary>
        /// <returns></returns>
        public string GetMetadataMessage()
        {
            var operationResult = GetOperationResult();
            return operationResult.Metadata?.Message;
        }

        /// <summary>
        /// Returns the result of the current operation as Object
        /// </summary>
        /// <returns></returns>
        object IHandlerContext.GetEntity()
        {
            return GetEntity();
        }

        /// <summary>
        /// Adds or Update instance for Entity 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void AddOrUpdateEntity(TEntity entity)
        {
            AddOrUpdateParameter(EntityKeyName, entity);
        }

        /// <summary>
        ///  Adds or update parameter UpdateViewModel in the Context of the WritableController
        /// </summary>
        /// <typeparam name="TUpdateViewModel"></typeparam>
        /// <param name="model"></param>
        internal void AddOrUpdateUpdateViewModel<TUpdateViewModel>(TUpdateViewModel model) where TUpdateViewModel : ViewModelBase, IHaveId, new()
        {
            AddOrUpdateParameter(UpdateModelKeyName, model);
        }

        /// <summary>
        ///  Adds or update parameter UpdateViewModel in the Context of the WritableController
        /// </summary>
        /// <typeparam name="TCreateViewModel"></typeparam>
        /// <param name="model"></param>
        internal void AddOrUpdateCreateViewModel<TCreateViewModel>(TCreateViewModel model) where TCreateViewModel : class, IViewModel, new()
        {
            AddOrUpdateParameter(CreateModelKeyName, model);
        }

        /// <summary>
        /// Returns OperationResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public OperationResult<TViewModel> GetOperationResult()
        {
            var operationResult = GetParamByName<OperationResult<TViewModel>>(OperationResultKeyName);
            if (operationResult == null)
            {
                throw new ArgumentNullException(OperationResultKeyName);
            }

            return operationResult;
        }
    }
}

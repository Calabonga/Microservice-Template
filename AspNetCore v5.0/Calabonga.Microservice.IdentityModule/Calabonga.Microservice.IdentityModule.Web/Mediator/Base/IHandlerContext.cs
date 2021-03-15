using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.OperationResults;
using System;
using System.Collections.Generic;

namespace Calabonga.Microservice.IdentityModule.Web.Mediator.Base
{
/// <summary>
    /// HandlerContext base 
    /// </summary>
    public interface IHandlerContext
    {
        /// <summary>
        /// Add object as TEntity
        /// </summary>
        /// <param name="entity"></param>
        void AddOrUpdateEntity(object entity);

        /// <summary>
        /// Add to OperationResult metadata message
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="dataObject"></param>
        void AddError(Exception exception, object dataObject = null);

        /// <summary>
        /// Add to OperationResult metadata message
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="dataObject"></param>
        void AddError(string errorMessage, object dataObject = null);

        /// <summary>
        /// Add to OperationResult metadata message
        /// </summary>
        /// <param name="warningMessage"></param>
        /// <param name="dataObject"></param>
        void AddWarning(string warningMessage, object dataObject = null);

        /// <summary>
        /// Add to OperationResult metadata message
        /// </summary>
        /// <param name="successMessage"></param>
        /// <param name="dataObject"></param>
        void AddSuccess(string successMessage, object dataObject = null);

        /// <summary>
        /// Add to OperationResult metadata message
        /// </summary>
        /// <param name="infoMessage"></param>
        void AddInfo(string infoMessage);

        /// <summary>
        /// Add to OperationResult metadata message
        /// </summary>
        /// <param name="infoMessage"></param>
        /// <param name="dataObject"></param>
        void AddInfo(string infoMessage, object dataObject);

        /// <summary>
        /// Add to writable context some object as parameter
        /// </summary>
        void AddOrUpdateParameter(string name, object value);

        #region AppendLog

        /// <summary>
        /// Appends logs message into OperationResult in the WritableContext
        /// </summary>
        /// <param name="message"></param>
        void AppendLog(string message);

        /// <summary>
        /// Appends logs message into OperationResult in the WritableContext
        /// </summary>
        /// <param name="logs"></param>
        void AppendLog(IEnumerable<string> logs);

        #endregion

        /// <summary>
        /// Returns Message for OperationResult from context of the WritableController
        /// </summary>
        /// <returns></returns>
        string GetMetadataMessage();

        /// <summary>
        /// Returns the result of the current operation as Object
        /// </summary>
        /// <returns></returns>
        object GetEntity();

        /// <summary>
        /// Returns ViewModel for creation in current operation
        /// </summary>
        /// <returns></returns>
        object GetCreateViewModel();

        /// <summary>
        /// Returns object from Context parameters by name and type
        /// </summary>
        /// <param name="objectParserResultName"></param>
        /// <returns></returns>
        object GetParamByName(string objectParserResultName);

        /// <summary>
        /// Return OperationResult for current operations context
        /// </summary>
        /// <returns></returns>
        object GetOperationResult();
    }

    /// <summary>
    /// Handler Context
    /// </summary>
    public interface IHandlerContext<in TEntity, TViewModel> : IHandlerContext
        where TEntity : Identity
    {
        /// <summary>
        /// Initiate new instance of the OperationResult or Update if exists
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        void InitOrUpdate(OperationResult<TViewModel> operationResult = null);

        /// <summary>
        /// Adds or updates Entity model for current controller <see cref="TEntity"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void AddOrUpdateEntity(TEntity entity);

        /// <summary>
        /// Creates UpdateViewModel for current controller <see cref="TCreateViewModel"/>
        /// </summary>
        /// <typeparam name="TCreateViewModel"></typeparam>
        /// <returns></returns>
        TCreateViewModel GetCreateViewModel<TCreateViewModel>();

        /// <summary>
        /// Returns UpdateViewModel for current controller <see cref="TUpdateViewModel"/>
        /// </summary>
        /// <typeparam name="TUpdateViewModel"></typeparam>
        /// <returns></returns>
        TUpdateViewModel GetUpdateViewModel<TUpdateViewModel>();

        /// <summary>
        /// Returns Parameter from controller context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyName"></param>
        /// <returns></returns>
        T GetParamByName<T>(string keyName);


        /// <summary>
        /// Returns OperationResult Result entity
        /// </summary>
        /// <returns></returns>
        TViewModel GetResult();

        /// <summary>
        /// Sets the Result for OperationResult
        /// </summary>
        /// <param name="result"></param>
        void SetOperationResult(TViewModel result);
    }
}

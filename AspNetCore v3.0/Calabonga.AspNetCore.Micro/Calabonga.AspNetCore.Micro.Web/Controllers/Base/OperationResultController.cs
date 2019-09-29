using System;
using Calabonga.OperationResultsCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.AspNetCore.Micro.Web.Controllers.Base
{
    /// <summary>
    /// Base controller for PricePoint wrapped with OperationResult
    /// </summary>
    [ApiController]
    [EnableCors("CorsPolicy")]
    public abstract class OperationResultController : Controller
    {
        /// <summary>
        /// OperationResult Response
        /// </summary>
        /// <param name="operationResult"></param>
        /// <returns></returns>
        [NonAction]
        protected ActionResult<OperationResult<TResult>> OperationResultResponse<TResult>(OperationResult<TResult> operationResult)
        {
            return Ok(operationResult);
        }

        /// <summary>
        /// OperationResult Response
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        protected ActionResult<OperationResult<TResult>> OperationResultInfo<TResult>(TResult result, string message)
        {
            var operation = OperationResult.CreateResult<TResult>();
            operation.Result = result;
            return OperationResultResponse(result, message, OperationInfo.Info);
        }

        /// <summary>
        /// OperationResult Response
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        protected ActionResult<OperationResult<TResult>> OperationResultSuccess<TResult>(TResult result)
        {
            var operation = OperationResult.CreateResult<TResult>();
            operation.Result = result;
            return OperationResultResponse(result, string.Empty, OperationInfo.Success);
        }
        
        /// <summary>
        /// OperationResult Response
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        protected ActionResult OperationResultSuccess<TResult>(TResult result, string message)
        {
            var operation = OperationResult.CreateResult<TResult>();
            operation.Result = result;
            return OperationResultResponse(result, message, OperationInfo.Success);
        }

        /// <summary>
        /// OperationResult Response
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        protected ActionResult<OperationResult<TResult>> OperationResultWarning<TResult>(TResult result, string message)
        {
            var operation = OperationResult.CreateResult<TResult>();
            operation.Result = result;
            return OperationResultResponse(result, message, OperationInfo.Warning);
        }

        /// <summary>
        /// OperationResult Response
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        protected ActionResult<OperationResult<TResult>> OperationResultError<TResult>(TResult result, string message)
        {
            var operation = OperationResult.CreateResult<TResult>();
            operation.Result = result;
            return OperationResultResponse(result, message, OperationInfo.Error);
        }

        /// <summary>
        /// OperationResult Response
        /// </summary>
        /// <param name="result"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        [NonAction]
        protected ActionResult<OperationResult<TResult>> OperationResultError<TResult>(TResult result, string[] messages)
        {
            var operation = OperationResult.CreateResult<TResult>();
            operation.Result = result;
            operation.AddError(string.Join(" | ", messages));
            return Ok(operation);
        }

        /// <summary>
        /// OperationResult Response
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        protected ActionResult<OperationResult<TResult>> OperationResultError<TResult>(TResult result)
        {
            var operation = OperationResult.CreateResult<TResult>();
            operation.Result = result;
            return OperationResultResponse(result, string.Empty, OperationInfo.Error);
        }

        /// <summary>
        /// OperationResult Response
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        protected ActionResult OperationResultError(string message)
        {
            var operation = OperationResult.CreateResult<bool>();
            operation.Result = false;
            return OperationResultResponse(string.Empty, message, OperationInfo.Error);
        }

        /// <summary>
        /// OperationResult Response
        /// </summary>
        /// <param name="result"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        [NonAction]
        protected ActionResult<OperationResult<TResult>> OperationResultError<TResult>(TResult result, Exception exception)
        {
            var operation = OperationResult.CreateResult<TResult>();
            operation.Result = result;
            return OperationResultResponse(result, exception.Message, OperationInfo.Error, exception);
        }

        /// <summary>
        /// OperationResult Response
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        [NonAction]
        protected ActionResult<OperationResult<TResult>> OperationResultError<TResult>(TResult result, string message, Exception exception)
        {
            var operation = OperationResult.CreateResult<TResult>();
            operation.Result = result;
            return OperationResultResponse(result, message, OperationInfo.Error, exception);
        }

        /// <summary>
        /// OperationResult Response
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        protected ActionResult<OperationResult<TResult>> OperationResultObject<TResult>(TResult result, string message)
        {
            var operation = OperationResult.CreateResult<TResult>();
            operation.Result = result;
            return OperationResultResponse(result, message, OperationInfo.DataObject);
        }

        /// <summary>
        /// OperationResult Response
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <param name="info"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        [NonAction]
        private ActionResult OperationResultResponse<TResult>(TResult result, string message, OperationInfo info, Exception exception = null)
        {
            var operation = OperationResult.CreateResult<TResult>();
            operation.Result = result;
            switch (info)
            {
                case OperationInfo.Info:
                    operation.AddInfo(message);
                    break;
                case OperationInfo.Success:
                    operation.AddSuccess(message);
                    break;
                case OperationInfo.Warning:
                    operation.AddWarning(message);
                    break;
                case OperationInfo.Error:
                    operation.AddError(message);
                    break;
                case OperationInfo.DataObject:
                    operation.AddData(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(info), info, null);
            }

            if (exception != null)
            {
                operation.Error = exception;
            }
            return Ok(operation);
        }
    }
}

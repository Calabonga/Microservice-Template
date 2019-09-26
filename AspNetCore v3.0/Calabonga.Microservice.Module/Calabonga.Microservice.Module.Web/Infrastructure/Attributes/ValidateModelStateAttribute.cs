using System.Linq;
using Calabonga.OperationResultsCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Calabonga.Microservice.Module.Web.Infrastructure.Attributes
{
    /// <summary>
    /// Custom validation handler for availability to whit OperationResult
    /// </summary>
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var operation = OperationResult.CreateResult<object>();
                operation.AddError(context.ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage)));
                context.Result = new OkObjectResult(operation);
            }
        }
    }
}

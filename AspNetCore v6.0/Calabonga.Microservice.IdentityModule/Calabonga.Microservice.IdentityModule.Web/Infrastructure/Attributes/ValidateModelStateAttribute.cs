using System.Linq;
using Calabonga.OperationResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Attributes
{
    /// <summary>
    /// Custom validation handler for availability to whit OperationResult
    /// </summary>
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var operation = OperationResult.CreateResult<object>();
            var messages = context.ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
            var message = string.Join(" ", messages);
            operation.AddError(message);
            context.Result = new OkObjectResult(operation);
        }
    }
}

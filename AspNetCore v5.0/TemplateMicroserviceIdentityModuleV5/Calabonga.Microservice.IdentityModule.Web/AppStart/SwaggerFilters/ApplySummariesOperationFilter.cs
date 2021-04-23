using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace $safeprojectname$.AppStart.SwaggerFilters
{
    /// <summary>
    /// Swagger Method Info Generator from summary for <see cref="M:GetPaged{T}"/>
    /// </summary>
    public class ApplySummariesOperationFilter : IOperationFilter
    {
        /// <inheritdoc />
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null)
            {
                return;
            }

            var actionName = controllerActionDescriptor.ActionName;
            if (actionName != "GetPaged")
            {
                return;
            }
            var resourceName = controllerActionDescriptor.ControllerName;
            operation.Summary = $"Returns paged list of the {resourceName} as IPagedList wrapped with OperationResult";
        }
    }
}
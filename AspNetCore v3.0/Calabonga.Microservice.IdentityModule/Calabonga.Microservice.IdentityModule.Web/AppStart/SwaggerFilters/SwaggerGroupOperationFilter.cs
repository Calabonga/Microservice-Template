using System.Linq;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Calabonga.Microservice.IdentityModule.Web.AppStart.SwaggerFilters
{
    /// <summary>
    /// Swagger operation filter for <see cref="SwaggerGroupAttribute"/>
    /// </summary>
    public class SwaggerGroupOperationFilter : IOperationFilter
    {
        /// <inheritdoc />
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var attributes = controllerActionDescriptor.EndpointMetadata.OfType<SwaggerGroupAttribute>().ToList();
                if (attributes.Any())
                {
                    var groupNameAttribute = attributes.First();
                    operation.Tags = new[] { groupNameAttribute.GroupName };
                }
                else
                {
                    operation.Tags = new[] { controllerActionDescriptor?.RouteValues["controller"] };
                }
            }
        }
    }
}

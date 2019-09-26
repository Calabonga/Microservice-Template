using System.Linq;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Calabonga.AspNetCore.Micro.Web.AppStart.SwaggerFilters
{
    /// <summary>
    /// Swagger operation filter for <see cref="SwaggerGroupAttribute"/>
    /// </summary>
    public class SwaggerGroupOperationFilter : IOperationFilter
    {
        /// <inheritdoc />
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var attributes = controllerActionDescriptor.EndpointMetadata.OfType<SwaggerGroupAttribute>().ToList();
                if (attributes.Any())
                {
                    var groupNameAttribute = attributes.First();
                    operation.Tags = new[] {
                        new OpenApiTag {Name = groupNameAttribute.GroupName}};
                }
                else
                {
                    operation.Tags = new[]
                    {
                        new OpenApiTag {Name = controllerActionDescriptor?.RouteValues["controller"]}};
                }
            }
        }
    }
}

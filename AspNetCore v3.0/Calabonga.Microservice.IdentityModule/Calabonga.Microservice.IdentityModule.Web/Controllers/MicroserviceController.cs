using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Calabonga.Microservice.IdentityModule.Web.Controllers
{
    /// <summary>
    /// Microservice information controller for main shell communications
    /// </summary>
    [Route("api/[controller]")]
    public class MicroserviceController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        /// <summary>
        /// Returns all endpoint specification for current microservice
        /// </summary>
        /// <param name="actionDescriptorCollectionProvider"></param>
        public MicroserviceController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        /// <summary>
        /// Returns information about service
        /// </summary>
        /// <returns></returns>
        [Route("info")]
        [HttpGet]
        public IActionResult GetModuleActions()
        {
            var descriptors = _actionDescriptorCollectionProvider
                .ActionDescriptors.Items
                .OfType<ControllerActionDescriptor>()
                .Where(c => c.ControllerTypeInfo.AsType() != typeof(MicroserviceController)).ToList();

            var data = descriptors.Select(a => new
            {
                Description = "Sample text (The description could be store in Resource.resx file for different localization)",
                a.ControllerName,
                AttributeRouteTemplate = a.AttributeRouteInfo?.Template.ToLower(),
                HttpMethods = string.Join(", ", a.ActionConstraints?.OfType<HttpMethodActionConstraint>().SingleOrDefault()?.HttpMethods ?? new[] { "any" }),
                Parameters = a.Parameters?.Select(p => new
                {
                    Type = p.ParameterType.Name,
                    p.Name
                }),
                a.MethodInfo?.GetCustomAttribute<AuthorizeAttribute>()?.Policy,
                ControllerClassName = a.ControllerTypeInfo.FullName,
                ActionMethodName = a.MethodInfo?.Name
            });
            return Ok(data);
        }
    }
}

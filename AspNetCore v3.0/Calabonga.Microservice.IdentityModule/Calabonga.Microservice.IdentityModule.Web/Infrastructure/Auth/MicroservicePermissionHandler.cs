using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Auth
{
    /// <summary>
    /// Permission handler for custom authorization implementations
    /// </summary>
    public class MicroservicePermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <inheritdoc />
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
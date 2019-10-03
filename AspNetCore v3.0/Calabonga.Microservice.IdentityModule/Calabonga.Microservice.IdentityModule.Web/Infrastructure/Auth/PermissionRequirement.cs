using Microsoft.AspNetCore.Authorization;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Auth
{
    /// <summary>
    /// Permission requirement for user or service authorization
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <inheritdoc />
        public PermissionRequirement(string permissionName)
        {
            PermissionName = permissionName;
        }

        /// <summary>
        /// Permission name
        /// </summary>
        public string PermissionName { get; }
    }
}
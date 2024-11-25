using Microsoft.AspNetCore.Authorization;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.OpenIddict;

/// <summary>
/// Permission requirement for user or service authorization
/// </summary>
public class PermissionRequirement(string permissionName) : IAuthorizationRequirement
{
    /// <summary>
    /// Permission name
    /// </summary>
    public string PermissionName { get; } = permissionName;
}
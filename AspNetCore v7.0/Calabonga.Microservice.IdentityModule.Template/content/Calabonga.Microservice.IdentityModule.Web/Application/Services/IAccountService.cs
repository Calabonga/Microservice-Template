using Calabonga.Microservice.IdentityModule.Infrastructure;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.ProfileEndpoints.ViewModels;
using Calabonga.Microservices.Core.Validators;
using Calabonga.OperationResults;
using System.Security.Claims;

namespace Calabonga.Microservice.IdentityModule.Web.Application.Services;

/// <summary>
/// Represent interface for account management
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Returns a collection of the <see cref="ApplicationUser"/> by emails
    /// </summary>
    /// <param name="emails"></param>

    Task<IEnumerable<ApplicationUser>> GetUsersByEmailsAsync(IEnumerable<string> emails);

    /// <summary>
    /// Get User Id from HttpContext
    /// </summary>

    Guid GetCurrentUserId();

    /// <summary>
    /// Returns <see cref="ApplicationUser"/> instance after successful registration
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>

    Task<OperationResult<UserProfileViewModel>> RegisterAsync(RegisterViewModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Returns User by user identifier
    /// </summary>
    /// <param name="id"></param>

    Task<ApplicationUser> GetByIdAsync(Guid id);

    /// <summary>
    /// Returns ClaimPrincipal by user identity
    /// </summary>
    /// <param name="identifier"></param>

    Task<ClaimsPrincipal> GetPrincipalByIdAsync(string identifier);

    /// <summary>
    /// Returns ClaimPrincipal by user identity
    /// </summary>
    /// <param name="user"></param>

    Task<ClaimsPrincipal> GetPrincipalForUserAsync(ApplicationUser user);

    /// <summary>
    /// Returns current user account information or null when user does not logged in
    /// </summary>

    Task<ApplicationUser> GetCurrentUserAsync();

    /// <summary>
    /// Check roles for current user
    /// </summary>
    /// <param name="roleNames"></param>

    Task<PermissionValidationResult> IsInRolesAsync(string[] roleNames);

    /// <summary>
    /// Returns all system administrators registered in the system
    /// </summary>
    /// <param name="roleName"></param>

    Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleName);
}
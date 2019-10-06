using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Calabonga.AspNetCore.Micro.Data;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Validations.Base;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.ViewModels.AccountViewModels;
using Calabonga.OperationResultsCore;

namespace Calabonga.AspNetCore.Micro.Web.Infrastructure.Services
{
    /// <summary>
    /// Represent interface for account management
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Returns a collection of the <see cref="ApplicationUser"/> by emails
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        Task<IEnumerable<ApplicationUser>> GetUsersByEmailsAsync(IEnumerable<string> emails);
        
        /// <summary>
        /// Get User Id from HttpContext
        /// </summary>
        /// <returns></returns>
        Guid GetCurrentUserId();

        /// <summary>
        /// Returns <see cref="ApplicationUser"/> instance after successful registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<OperationResult<ApplicationUserProfileViewModel>> RegisterAsync(RegisterViewModel model);

        /// <summary>
        /// Returns user profile
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Task<OperationResult<ApplicationUserProfileViewModel>> GetProfileAsync(string identifier);

        /// <summary>
        /// Returns User by user identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApplicationUser> GetByIdAsync(Guid id);

        /// <summary>
        /// Returns ClaimPrincipal by user identity
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Task<ClaimsPrincipal> GetUserClaimsAsync(string identifier);

        /// <summary>
        /// Returns current user account information or null when user does not logged in
        /// </summary>
        /// <returns></returns>
        Task<ApplicationUser> GetCurrentUserAsync();

        /// <summary>
        /// Check roles for current user
        /// </summary>
        /// <param name="roleNames"></param>
        /// <returns></returns>
        Task<PermissionValidationResult> IsInRolesAsync(string[] roleNames);

        /// <summary>
        /// Returns all system administrators registered in the system
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleName);
    }
}

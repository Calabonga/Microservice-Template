using Calabonga.AuthService.Infrastructure;
using Calabonga.AuthService.Web.Endpoints.ProfileEndpoints.ViewModels;
using Calabonga.Microservices.Core.Validators;
using Calabonga.OperationResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Calabonga.AuthService.Web.Application.Services;

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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<OperationResult<UserProfileViewModel>> RegisterAsync(RegisterViewModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Returns user profile
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    Task<OperationResult<UserProfileViewModel>> GetProfileByIdAsync(string identifier);

    /// <summary>
    /// Returns user profile
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<OperationResult<UserProfileViewModel>> GetProfileByEmailAsync(string email);

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
    Task<ClaimsPrincipal> GetUserClaimsByIdAsync(string identifier);

    /// <summary>
    /// Returns ClaimPrincipal by user identity
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<ClaimsPrincipal> GetUserClaimsByEmailAsync(string email);

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

/// <summary>
/// Data transfer object for user registration
/// // Calabonga: move to folder (2022-05-07 11:11 IAccountService)
/// </summary>
public class RegisterViewModel
{
    /// <summary>
    /// FirstName
    /// </summary>
    [Required]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    /// <summary>
    /// LastName
    /// </summary>
    [Required]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    /// <summary>
    /// Password confirmation
    /// </summary>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}

/// <summary>
/// // Calabonga: move to folder (2022-05-07 11:11 IAccountService)
/// </summary>
public class ApplicationClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
{
    /// <inheritdoc />
    public ApplicationClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
    {

    }

    /// <summary>
    /// Creates a <see cref="T:System.Security.Claims.ClaimsPrincipal" /> from an user asynchronously.
    /// </summary>
    /// <param name="user">The user to create a <see cref="T:System.Security.Claims.ClaimsPrincipal" /> from.</param>
    /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous creation operation, containing the created <see cref="T:System.Security.Claims.ClaimsPrincipal" />.</returns>
    public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await base.CreateAsync(user);

        if (user.ApplicationUserProfile?.Permissions != null)
        {
            var permissions = user.ApplicationUserProfile.Permissions.ToList();
            if (permissions.Any())
            {
                permissions.ForEach(x => ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim(x.PolicyName, ClaimTypes.Role)));
            }
        }
        if (!string.IsNullOrWhiteSpace(user.FirstName))
        {
            ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(user.LastName))
        {
            ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
        }

        return principal;
    }

}
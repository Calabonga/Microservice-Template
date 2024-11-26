using Calabonga.Microservice.IdentityModule.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Authorizations;

/// <summary>
/// User Claims Principal Factory override from Microsoft Identity framework
/// </summary>
public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
{
    /// <inheritdoc />
    public ApplicationUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor) { }

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
                permissions.ForEach(x => ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim(x.PolicyName, nameof(x.PolicyName).ToLower())));
            }
        }

        ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim("framework", "nimble"));

        if (!string.IsNullOrWhiteSpace(user.UserName))
        {
            ((ClaimsIdentity)principal.Identity!).AddClaim(new Claim(ClaimTypes.Name, user.UserName));
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
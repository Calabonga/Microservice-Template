using System.Security.Claims;
using System.Threading.Tasks;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.Microservice.IdentityModule.Data;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Auth
{
    /// <summary>
    /// Application Claims custom factory
    /// </summary>
    public class ApplicationClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        private readonly IUnitOfWork<ApplicationUser, ApplicationRole> _initOfWork;

        /// <inheritdoc />
        public ApplicationClaimsPrincipalFactory(IUnitOfWork<ApplicationUser, ApplicationRole> initOfWork, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
            _initOfWork = initOfWork;
        }

        /// <summary>
        /// Creates a <see cref="T:System.Security.Claims.ClaimsPrincipal" /> from an user asynchronously.
        /// </summary>
        /// <param name="user">The user to create a <see cref="T:System.Security.Claims.ClaimsPrincipal" /> from.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous creation operation, containing the created <see cref="T:System.Security.Claims.ClaimsPrincipal" />.</returns>
        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrWhiteSpace(user.AdditionalEmails))
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("AdditionalEmails", user.AdditionalEmails));
            }

            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(JwtClaimTypes.GivenName, user.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
            }
            
            return principal;
        }

    }
}
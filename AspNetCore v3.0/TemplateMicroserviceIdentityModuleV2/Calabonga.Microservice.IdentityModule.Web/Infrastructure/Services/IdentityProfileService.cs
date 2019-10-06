using System.Linq;
using System.Threading.Tasks;
using Calabonga.Microservices.Core.Extensions;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace $safeprojectname$.Infrastructure.Services
{
    /// <summary>
    /// Custom implementation of the IProfileService
    /// </summary>
    public class IdentityProfileService : IProfileService
    {
        private readonly IAccountService _accountService;
        /// <summary>
        /// Identity Profile Service
        /// </summary>
        /// <param name="accountService"></param>
        public IdentityProfileService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the user info endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var identifier = context.Subject.GetSubjectId();
            var profile = await _accountService.GetUserClaimsAsync(identifier);
            context.IssuedClaims = profile.Claims.ToList();
        }

        /// <summary>
        /// Returns IsActive (IdentityServer4)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var identifier = context.Subject.GetSubjectId();
            var user = await _accountService.GetByIdAsync(identifier.ToGuid());
            context.IsActive = user != null;
        }
    }
}

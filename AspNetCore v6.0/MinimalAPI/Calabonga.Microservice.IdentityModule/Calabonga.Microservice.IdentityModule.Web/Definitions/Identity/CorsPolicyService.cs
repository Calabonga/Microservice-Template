using IdentityServer4.Services;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Identity
{
    public class CorsPolicyService : ICorsPolicyService
    {
        /// <summary>Determines whether origin is allowed.</summary>
        /// <param name="origin">The origin.</param>
        /// <returns></returns>
        public Task<bool> IsOriginAllowedAsync(string origin) =>
            Task.FromResult(true);
    }
}

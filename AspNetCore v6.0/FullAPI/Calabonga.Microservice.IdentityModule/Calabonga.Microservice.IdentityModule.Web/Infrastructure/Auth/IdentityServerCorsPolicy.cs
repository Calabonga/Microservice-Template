using IdentityServer4.Services;
using System.Threading.Tasks;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Auth
{
    /// <summary>
    /// Customized CORS policy for IdentityServer4
    /// </summary>
    public class IdentityServerCorsPolicy : ICorsPolicyService
    {
        /// <inheritdoc />
        public Task<bool> IsOriginAllowedAsync(string origin) => Task.FromResult(true);
    }
}
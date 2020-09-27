using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$.Infrastructure.Auth
{
    /// <summary>
    /// Authorization Policy registration
    /// </summary>
    public static class AuthorizationPolicyExtensions
    {
        /// <summary>
        /// Registers custom <see cref="IAuthorizationHandler"/> for able to use authorization by policy 
        /// </summary>
        /// <param name="services"></param>
        public static void UseMicroserviceAuthorizationPolicy(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, MicroservicePermissionHandler>();
        }
    }
}

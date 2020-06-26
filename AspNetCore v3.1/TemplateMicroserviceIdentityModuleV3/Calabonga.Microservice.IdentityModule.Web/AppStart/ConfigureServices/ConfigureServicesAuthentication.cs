using $ext_projectname$.Core;
using $ext_projectname$.Data;
using $ext_projectname$.Web.Infrastructure.Services;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace $ext_projectname$.Web.AppStart.ConfigureServices
{
    /// <summary>
    /// ASP.NET Core services registration and configurations
    /// Authentication path
    /// </summary>
    public static class ConfigureServicesAuthentication
    {
        /// <summary>
        /// Configure Authentication & Authorization
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
             var url = configuration.GetSection("IdentityServer").GetValue<string>("Url");
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
               .AddIdentityServerAuthentication(options =>
               {
                   options.SupportedTokens = SupportedTokens.Jwt;
                   options.Authority = $"{url}{AppData.AuthUrl}";
                   options.EnableCaching = true;
                   options.RequireHttpsMetadata = false;
               });

            services.AddIdentityServer(options =>
                {
                    options.Authentication.CookieSlidingExpiration = true;
                    options.IssuerUri = $"{url}{AppData.AuthUrl}";
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddInMemoryPersistedGrants()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddAspNetIdentity<ApplicationUser>()
                .AddJwtBearerClientAuthentication()
                .AddProfileService<IdentityProfileService>();
        }
    }
}

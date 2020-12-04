using Calabonga.Microservice.Module.Core;
using Calabonga.Microservice.Module.Web.Infrastructure.Auth;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Microservice.Module.Web.AppStart.ConfigureServices
{
    /// <summary>
    /// ASP.NET Core services registration and configurations
    /// </summary>
    public static class ConfigureServicesAuthentication
    {
        /// <summary>
        /// ConfigureServices Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration.GetSection("IdentityServer").GetValue<string>("Url");

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.SupportedTokens = SupportedTokens.Jwt;
                    options.Authority = $"{url}";
                    options.EnableCaching = true;
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization();
            
            services.AddSingleton<IAuthorizationHandler, MicroservicePermissionHandler>();
        }
    }
}
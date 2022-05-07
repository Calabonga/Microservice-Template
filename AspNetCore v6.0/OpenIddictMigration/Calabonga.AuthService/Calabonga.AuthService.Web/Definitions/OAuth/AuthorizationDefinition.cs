using Calabonga.AuthService.Web.Definitions.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;

namespace Calabonga.AuthService.Web.Definitions.OAuth
{
    /// <summary>
    /// Authorization Policy registration
    /// </summary>
    public class AuthorizationDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = OpenIddictConstants.Schemes.Bearer;
                    options.DefaultAuthenticateScheme =  OpenIddictConstants.Schemes.Bearer;
                    options.DefaultChallengeScheme = OpenIddictConstants.Schemes.Bearer;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    cfg.Audience = "https://localhost:4200/";
                    cfg.Authority = "https://localhost:5000/";
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters();
                    cfg.Configuration = new OpenIdConnectConfiguration();  
                });

            services.AddAuthorization();
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, MicroservicePermissionHandler>();
        }

        /// <summary>
        /// Configure application for current application
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            // registering UserIdentity helper as singleton
            UserIdentity.Instance.Configure(app.Services.GetService<IHttpContextAccessor>()!);
        }
    }
}
using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.IdentityModule.Domain.Base;
using Calabonga.Microservice.IdentityModule.Web.Definitions.OpenIddict;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Identity;

/// <summary>
/// Authorization Policy registration
/// </summary>
public class AuthorizationDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current application
    /// </summary>
    /// <param name="builder"></param>
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/connect/login";
            });

        builder.Services.AddAuthorization(options =>
        {
            // Policy by default all methods requires an authenticated user.
            // That's mean that not anonymous user allowed and additional
            // permission should be granted using AllowAnonymous()
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            options.AddPolicy(AuthData.AuthSchemes, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "api");
            });
        });

        builder.Services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
        builder.Services.AddSingleton<IAuthorizationHandler, AppPermissionHandler>();
    }

    /// <summary>
    /// Configure application for current application
    /// </summary>
    /// <param name="app"></param>
    public override void ConfigureApplication(WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(AppData.PolicyName);
        app.UseAuthentication();
        app.UseAuthorization();

        // registering UserIdentity helper as singleton
        UserIdentity.Instance.Configure(app.Services.GetService<IHttpContextAccessor>()!);
    }
}
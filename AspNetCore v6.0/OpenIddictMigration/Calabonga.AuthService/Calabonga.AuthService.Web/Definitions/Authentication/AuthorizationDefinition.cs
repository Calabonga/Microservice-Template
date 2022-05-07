using Calabonga.AuthService.Web.Definitions.Base;
using Calabonga.AuthService.Web.Definitions.OpenIddict;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;

namespace Calabonga.AuthService.Web.Definitions.Authentication;

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
                options.DefaultAuthenticateScheme = OpenIddictConstants.Schemes.Bearer;
                options.DefaultChallengeScheme = OpenIddictConstants.Schemes.Bearer;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Connect/Login";
                options.LogoutPath = "/Connect/Logout";
            })
            // .AddJwtBearer(options =>
            // {
            // options.Authority = "https://localhost:20001";
            // options.Audience = "https://localhost:20001";
            // })
            ;

        services.AddAuthorization();
        services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
        services.AddSingleton<IAuthorizationHandler, AppPermissionHandler>();
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
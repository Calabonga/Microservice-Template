using Calabonga.AspNetCore.AppDefinitions;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Calabonga.AspNetCoreRazorPages.Web.Definitions.Identity;

/// <summary>
/// Authorization Policy registration
/// </summary>
public class AuthorizationDefinition : AppDefinition
{
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })

        .AddCookie(options =>
        {
            options.LoginPath = "/connect/login";
            options.LogoutPath = "/connect/logout";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
            options.SlidingExpiration = false;
        });

    }
}

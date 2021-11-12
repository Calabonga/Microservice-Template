using Microsoft.AspNetCore.Builder;

namespace Calabonga.Microservice.IdentityModule.Web.AppStart.Configures;

/// <summary>
/// Configure pipeline
/// </summary>
public static class ConfigureAuthentication
{
    /// <summary>
    /// Configure Routing
    /// </summary>
    /// <param name="app"></param>
    public static void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
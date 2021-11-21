using Microsoft.AspNetCore.Builder;

namespace $safeprojectname$.AppStart.Configures;

/// <summary>
/// Configure pipeline
/// </summary>
public static class ConfigureEndpoints
{
    /// <summary>
    /// Configure Routing
    /// </summary>
    /// <param name="app"></param>
    public static void Configure(IApplicationBuilder app) => app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
    });
}
using Calabonga.AspNetCore.AppDefinitions;
using $ext_projectname$.Domain.Base;

namespace $safeprojectname$.Definitions.Cors;

/// <summary>
/// Cors configurations
/// </summary>
public class CorsDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current application
    /// </summary>
    /// <param name="builder"></param>
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        var origins = builder.Configuration.GetSection("Cors")?.GetSection("Origins")?.Value?.Split(',');
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(AppData.PolicyName, policyBuilder =>
            {
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyMethod();
                if (origins is not { Length: > 0 })
                {
                    return;
                }

                if (origins.Contains("*"))
                {
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.SetIsOriginAllowed(host => true);
                    policyBuilder.AllowCredentials();
                }
                else
                {
                    foreach (var origin in origins)
                    {
                        policyBuilder.WithOrigins(origin);
                    }
                }
            });
        });
    }
}
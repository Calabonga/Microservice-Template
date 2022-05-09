using Calabonga.Microservice.Module.Domain.Base;
using Calabonga.Microservice.Module.Web.Definitions.Base;

namespace Calabonga.Microservice.Module.Web.Definitions.Cors;

/// <summary>
/// Cors configurations
/// </summary>
public class CorsDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current microservice
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var origins = configuration.GetSection("Cors")?.GetSection("Origins")?.Value?.Split(',');
        services.AddCors(options =>
        {
            options.AddPolicy(AppData.PolicyName, builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                if (origins != null && origins.Length > 0)
                {
                    if (origins.Contains("*"))
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.SetIsOriginAllowed(host => true);
                        builder.AllowCredentials();
                    }
                    else
                    {
                        foreach (var origin in origins)
                        {
                            builder.WithOrigins(origin);
                        }
                    }
                }
            });
        });
    }
}
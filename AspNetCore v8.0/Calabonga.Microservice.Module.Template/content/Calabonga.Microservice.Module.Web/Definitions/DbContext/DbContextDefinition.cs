using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.Module.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Microservice.Module.Web.Definitions.DbContext;

/// <summary>
/// ASP.NET Core services registration and configurations
/// </summary>
public class DbContextDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current microservice
    /// </summary>
    /// <param name="builder"></param>
    public override void ConfigureServices(WebApplicationBuilder builder)
        => builder.Services.AddDbContext<ApplicationDbContext>(config =>
        {
            // ATTENTION!
            // UseInMemoryDatabase - This is for demo purposes only!
            // Should uninstall package "Microsoft.EntityFrameworkCore.InMemory" and install what you need. 
            // For example: "Npgsql.EntityFrameworkCore.PostgreSQL" or "Microsoft.EntityFrameworkCore.SqlServer"
            config.UseInMemoryDatabase("DEMO_PURPOSES_ONLY");

            // uncomment line below to use UseNpgsql() or UseSqlServer(). Don't forget setup connection string in appSettings.json
            // config.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ApplicationDbContext)));
        });
}
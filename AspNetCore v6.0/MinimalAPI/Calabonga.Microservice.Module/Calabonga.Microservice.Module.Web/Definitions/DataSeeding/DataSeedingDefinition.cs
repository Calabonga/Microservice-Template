using Calabonga.Microservice.Module.Infrastructure.DatabaseInitialization;
using Calabonga.Microservice.Module.Web.Definitions.Base;

namespace Calabonga.Microservice.Module.Web.Definitions.DataSeeding;

/// <summary>
/// Seeding DbContext for default data for EntityFrameworkCore
/// </summary>
public class DataSeedingDefinition : AppDefinition
{
    /// <summary>
    /// Configure application for current microservice
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
        => DatabaseInitializer.Seed(app.Services);
}
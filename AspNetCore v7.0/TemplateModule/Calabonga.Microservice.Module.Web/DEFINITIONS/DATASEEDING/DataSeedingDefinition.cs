using Calabonga.AspNetCore.AppDefinitions;
using $ext_projectname$.Infrastructure.DatabaseInitialization;

namespace $safeprojectname$.Definitions.DataSeeding;

/// <summary>
/// Seeding DbContext for default data for EntityFrameworkCore
/// </summary>
public class DataSeedingDefinition : AppDefinition
{
    /// <summary>
    /// Configure application for current microservice
    /// </summary>
    /// <param name="app"></param>
    public override void ConfigureApplication(WebApplication app)
        => DatabaseInitializer.Seed(app.Services);
}
using Calabonga.AspNetCore.AppDefinitions;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Mapping;

/// <summary>
/// Register Automapper as application definition
/// </summary>
public class AutomapperDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current application
    /// </summary>
    /// <param name="builder"></param>
    public override void ConfigureServices(WebApplicationBuilder builder)
        => builder.Services.AddAutoMapper(typeof(Program));

    /// <summary>
    /// Configure application for current application
    /// </summary>
    /// <param name="app"></param>
    public override void ConfigureApplication(WebApplication app)
    {
        var mapper = app.Services.GetRequiredService<AutoMapper.IConfigurationProvider>();
        if (app.Environment.IsDevelopment())
        {
            // validate Mapper Configuration
            mapper.AssertConfigurationIsValid();
        }
        else
        {
            mapper.CompileMappings();
        }
    }
}
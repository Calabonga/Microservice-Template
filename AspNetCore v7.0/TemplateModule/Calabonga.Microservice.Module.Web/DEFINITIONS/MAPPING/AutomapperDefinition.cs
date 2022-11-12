using Calabonga.AspNetCore.AppDefinitions;

namespace $safeprojectname$.Definitions.Mapping;

/// <summary>
/// Register Automapper as MicroserviceDefinition
/// </summary>
public class AutomapperDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current microservice
    /// </summary>
    /// <param name="services"></param>
    /// <param name="builder"></param>
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
        => services.AddAutoMapper(typeof(Program));

    /// <summary>
    /// Configure application for current microservice
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
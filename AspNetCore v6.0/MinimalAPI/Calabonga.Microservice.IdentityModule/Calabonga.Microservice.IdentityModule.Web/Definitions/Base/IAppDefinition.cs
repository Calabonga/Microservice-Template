namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Base;

/// <summary>
/// Application definition interface abstraction
/// </summary>
internal interface IAppDefinition
{
    /// <summary>
    /// Configure services for current application
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);

    /// <summary>
    /// Configure application for current application
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    void ConfigureApplication(WebApplication app, IWebHostEnvironment env);
}
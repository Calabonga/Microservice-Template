namespace $safeprojectname$.Definitions.Base;

/// <summary>
/// Base implementation for <see cref="IMicroserviceDefinition"/>
/// </summary>
public abstract class MicroserviceDefinition : IMicroserviceDefinition
{
    /// <summary>
    /// Configure services for current microservice
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration) {}

    /// <summary>
    /// Configure application for current microservice
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public virtual void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env) { }
}
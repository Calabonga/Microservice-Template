using Calabonga.Microservice.Module.Web.Application;
using Calabonga.Microservice.Module.Web.Definitions.Base;
using MediatR;
using System.Reflection;

namespace Calabonga.Microservice.Module.Web.Definitions.Mediator;

/// <summary>
/// Register Mediator as MicroserviceDefinition
/// </summary>
public class MediatorDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current microservice
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}
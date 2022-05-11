using Calabonga.Microservice.IdentityModule.Web.Application;
using Calabonga.Microservice.IdentityModule.Web.Definitions.Base;
using MediatR;
using System.Reflection;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Mediator;

/// <summary>
/// Register Mediator as application definition
/// </summary>
public class MediatorDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current application
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}
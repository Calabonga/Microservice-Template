using $safeprojectname$.Application.Mediator.Behaviors;
using $safeprojectname$.Definitions.Base;
using MediatR;
using System.Reflection;

namespace $safeprojectname$.Definitions.Mediator;

/// <summary>
/// Register Mediator as MicroserviceDefinition
/// </summary>
public class MediatorMicroserviceDefinition : MicroserviceDefinition
{
    /// <summary>
    /// Configure services for current microservice
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        
        //services.AddScoped<IPipelineBehavior<LogPostRequest, OperationResult<LogViewModel>>, TransactionBehavior<LogPostRequest, OperationResult<LogViewModel>>>();

        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}
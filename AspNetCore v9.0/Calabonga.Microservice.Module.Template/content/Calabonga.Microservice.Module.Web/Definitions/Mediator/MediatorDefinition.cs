﻿using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.Module.Web.Definitions.FluentValidating;
using Mediator;


namespace Calabonga.Microservice.Module.Web.Definitions.Mediator;

/// <summary>
/// Register Mediator as MicroserviceDefinition
/// </summary>
public class MediatorDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current microservice
    /// </summary>
    /// <param name="builder"></param>
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
    }
}

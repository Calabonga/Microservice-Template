using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.IdentityModule.Domain.Base;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.ProfileMessages.Queries;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.ProfileMessages.ViewModels;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints;

public sealed class ProfilesEndpointDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        var group = app.MapGroup("/api/profiles").WithTags("Profiles");

        group.MapGet("roles", async ([FromServices] IMediator mediator, HttpContext context)
                => await mediator.Send(new GetProfile.Request(), context.RequestAborted))
            .RequireAuthorization(AppData.PolicyDefaultName)
            .RequireAuthorization(x =>
            {
                x.RequireClaim("Profiles:Roles:Get");
            })
            .Produces(200)
            .ProducesProblem(401);

        group.MapPost("register", async ([FromServices] IMediator mediator, [FromBody] RegisterViewModel model, HttpContext context)
                => await mediator.Send(new RegisterAccount.Request(model), context.RequestAborted))
            .Produces(200)
            .Produces<RegisterViewModel>()
            .AllowAnonymous();
    }
}

using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.ProfileMessages.Queries;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.ProfileMessages.ViewModels;
using Calabonga.Microservice.IdentityModule.Web.Definitions.OpenIddict;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints;

public sealed class ProfilesEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapGet("/api/profiles/roles", async ([FromServices] IMediator mediator, HttpContext context)
                => await mediator.Send(new GetProfile.Request(), context.RequestAborted))
            .RequireAuthorization(x =>
                x.AddAuthenticationSchemes(AuthData.AuthSchemes)
                    .RequireClaim("Profiles:Roles:Get"))
            .Produces(200)
            .ProducesProblem(401)
            .WithTags("Profiles")
            .WithOpenApi();

        app.MapPost("/api/profiles/register", async ([FromServices] IMediator mediator, RegisterViewModel model, HttpContext context)
                => await mediator.Send(new RegisterAccount.Request(model), context.RequestAborted))
            .Produces(200)
            .ProducesProblem(401)
            .WithTags("Profiles")
            .WithOpenApi()
            .AllowAnonymous();
    }
}
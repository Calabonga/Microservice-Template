using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.Module.Web.Application.Messaging.ProfileMessages.Queries;
using Calabonga.Microservice.Module.Web.Definitions.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.Module.Web.Endpoints;

public class ProfilesEndpointDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
        => app.MapGet("/api/profiles/roles", async ([FromServices] IMediator mediator, HttpContext context)
                => await mediator.Send(new GetProfile.Request(), context.RequestAborted))
            .RequireAuthorization(x =>
                x.AddAuthenticationSchemes(AuthData.AuthSchemes)
                    .RequireClaim("Profiles:Roles:Get"))
            .Produces(200)
            .ProducesProblem(401)
            .WithTags("Profiles")
            .WithOpenApi();
}
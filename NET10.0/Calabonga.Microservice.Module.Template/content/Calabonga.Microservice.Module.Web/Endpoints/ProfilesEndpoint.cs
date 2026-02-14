using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.Module.Web.Application.Messaging.ProfileMessages.Queries;
using Calabonga.Microservice.Module.Web.Definitions.Authorizations;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.Module.Web.Endpoints;

public sealed class ProfilesEndpointDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
        => app.MapProfilesEndpoints();
}

internal static class ProfilesEndpointDefinitionExtensions
{
    public static void MapProfilesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/profiles").WithTags("Profiles");

        group.MapGet("roles", async ([FromServices] IMediator mediator, HttpContext context)
                => await mediator.Send(new GetProfile.Request(), context.RequestAborted))
            .RequireAuthorization(AuthData.AuthSchemes)
            .RequireAuthorization(x =>
            {
                x.RequireClaim("Profiles:Roles:Get");
            })
            .Produces(200)
            .ProducesProblem(401);
    }
}

using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.Module.Web.Application.Messaging.ProfileMessages.Queries;
using Calabonga.Microservice.Module.Web.Definitions.Identity;
using Calabonga.Microservices.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.Module.Web.Endpoints;

public class ProfilesEndpointDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
        => app.MapGet("/api/profiles/get-roles", GetRoles);

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes, Policy = "EventItems:UserRoles:View")]
    [FeatureGroupName("Profiles")]
    private async Task<string> GetRoles([FromServices] IMediator mediator, HttpContext context)
        => await mediator.Send(new GetProfile.Request(), context.RequestAborted);
}
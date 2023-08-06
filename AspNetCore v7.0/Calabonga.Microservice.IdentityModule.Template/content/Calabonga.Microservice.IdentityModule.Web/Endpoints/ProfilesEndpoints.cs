using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.ProfileEndpoints.Queries;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.ProfileEndpoints.ViewModels;
using Calabonga.Microservice.IdentityModule.Web.Definitions.OpenIddict;
using Calabonga.Microservices.Core;
using Calabonga.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints;

public class ProfilesEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapGet("/api/profiles/get-roles", GetRoles);
        app.MapPost("/api/profiles/register", RegisterAccount);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [FeatureGroupName("Profiles")]
    private async Task<string> GetRoles([FromServices] IMediator mediator, HttpContext context)
        => await mediator.Send(new GetProfile.Request(), context.RequestAborted);

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Profiles")]
    private async Task<OperationResult<UserProfileViewModel>> RegisterAccount([FromServices] IMediator mediator, RegisterViewModel model, HttpContext context)
        => await mediator.Send(new RegisterAccount.Request(model), context.RequestAborted);
}
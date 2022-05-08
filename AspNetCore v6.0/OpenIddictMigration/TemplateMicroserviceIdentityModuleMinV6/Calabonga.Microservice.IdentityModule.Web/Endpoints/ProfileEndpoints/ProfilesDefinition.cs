using $safeprojectname$.Application;
using $safeprojectname$.Definitions.Base;
using $safeprojectname$.Definitions.OpenIddict;
using $safeprojectname$.Endpoints.ProfileEndpoints.Queries;
using $safeprojectname$.Endpoints.ProfileEndpoints.ViewModels;
using Calabonga.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace $safeprojectname$.Endpoints.ProfileEndpoints;

public class ProfilesDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.MapGet("/api/profiles/get-roles", GetRoles);
        app.MapPost("/api/profiles/register", RegisterAccount);
    }
    
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [FeatureGroupName("Profiles")]
    private async Task<string> GetRoles([FromServices] IMediator mediator, HttpContext context)
        => await mediator.Send(new GetRolesRequest(), context.RequestAborted);

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Profiles")]
    private async Task<OperationResult<UserProfileViewModel>> RegisterAccount([FromServices] IMediator mediator, RegisterViewModel model, HttpContext context)
        => await mediator.Send(new RegisterAccountRequest(model), context.RequestAborted);
}
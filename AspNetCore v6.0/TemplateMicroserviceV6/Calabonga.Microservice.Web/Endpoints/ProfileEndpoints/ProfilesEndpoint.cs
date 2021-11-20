using $safeprojectname$.Application.Services;
using $safeprojectname$.Definitions.Identity;
using $safeprojectname$.Endpoints.Base;
using $safeprojectname$.Endpoints.ProfileEndpoints.Queries;
using Calabonga.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace $safeprojectname$.Endpoints.ProfileEndpoints;

public class ProfilesEndpoint: IEndpoint
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration) 
        => services.AddTransient<IAccountService, AccountService>();

    public void ConfigureApplication(WebApplication app)
    {
        app.MapPost("/api/profiles/register", RegisterAccount);
        app.MapGet("/api/profiles/get-roles", GetRoles);
    }
    
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Profiles")]
    private async Task<OperationResult<UserProfileViewModel>> RegisterAccount([FromServices] IMediator mediator, RegisterViewModel model, HttpContext context)
        => await mediator.Send(new RegisterAccountRequest(model), context.RequestAborted);

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    [FeatureGroupName("Profiles")]
    private async Task<string> GetRoles([FromServices] IMediator mediator, HttpContext context)
        => await mediator.Send(new GetRolesRequest(), context.RequestAborted);
}
using Calabonga.Microservice.IdentityModule.Web.Application.Services;
using Calabonga.Microservice.IdentityModule.Web.Definitions.Identity;
using Calabonga.Microservice.IdentityModule.Web.Endpoints.Base;
using Calabonga.Microservice.IdentityModule.Web.Endpoints.ProfileEndpoints.Queries;
using Calabonga.Microservice.IdentityModule.Web.Endpoints.ProfileEndpoints.ViewModels;
using Calabonga.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints.ProfileEndpoints
{
    public class ProfilesEndpointDefinition : EndpointDefinition
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
            => services.AddTransient<IAccountService, AccountService>();

        public override void ConfigureApplication(WebApplication app)
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
}
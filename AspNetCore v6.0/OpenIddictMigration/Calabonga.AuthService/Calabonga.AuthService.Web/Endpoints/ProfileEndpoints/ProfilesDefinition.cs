using Calabonga.AuthService.Web.Application;
using Calabonga.AuthService.Web.Definitions.Base;
using Calabonga.AuthService.Web.Definitions.OpenIddict;
using Calabonga.AuthService.Web.Endpoints.ProfileEndpoints.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.AuthService.Web.Endpoints.ProfileEndpoints
{
    public class ProfilesDefinition : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env) => 
            app.MapGet("/api/profiles/get-roles", GetRoles);


        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
        [FeatureGroupName("Profiles")]
        private async Task<string> GetRoles([FromServices] IMediator mediator, HttpContext context)
            => await mediator.Send(new GetRolesRequest(), context.RequestAborted);
    }
}
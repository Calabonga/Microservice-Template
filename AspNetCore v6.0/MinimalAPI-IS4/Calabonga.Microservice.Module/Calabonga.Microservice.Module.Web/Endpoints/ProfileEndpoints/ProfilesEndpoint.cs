using Calabonga.Microservice.Module.Web.Definitions.Identity;
using Calabonga.Microservice.Module.Web.Endpoints.Base;
using Calabonga.Microservice.Module.Web.Endpoints.ProfileEndpoints.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Microservice.Module.Web.Endpoints.ProfileEndpoints
{
    public class ProfilesEndpointDefinition : EndpointDefinition
    {
        public override void ConfigureApplication(WebApplication app)
            => app.MapGet("/api/profiles/get-roles", GetRoles);
        
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
        [FeatureGroupName("Profiles")]
        private async Task<string> GetRoles([FromServices] IMediator mediator, HttpContext context)
            => await mediator.Send(new GetRolesRequest(), context.RequestAborted);
    }
}
using Calabonga.Microservices.Core;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Microservice.Module.Web.Endpoints.ProfileEndpoints.Queries
{
    /// <summary>
    /// Request: Returns user roles 
    /// </summary>
    public record GetRolesRequest : IRequest<string>;

    public class GetRolesRequestHandler : RequestHandler<GetRolesRequest, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetRolesRequestHandler(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        protected override string Handle(GetRolesRequest request)
        {
            var user = _httpContextAccessor.HttpContext!.User;
            var roles = ClaimsHelper.GetValues<string>((ClaimsIdentity)user.Identity!, "role");
            return $"Current user ({user.Identity!.Name}) have following roles: {string.Join("|", roles)}";
        }
    }
}
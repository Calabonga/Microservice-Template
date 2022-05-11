using Calabonga.Microservice.IdentityModule.Infrastructure;
using Calabonga.Microservices.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Identity.Queries
{
    public record GetUserClaimsByIdRequest(string UserId) : IRequest<ClaimsPrincipal>;

    public class GetUserClaimsByIdRequestHandler : IRequestHandler<GetUserClaimsByIdRequest, ClaimsPrincipal>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationClaimsPrincipalFactory _claimsFactory;

        public GetUserClaimsByIdRequestHandler(UserManager<ApplicationUser> userManager, ApplicationClaimsPrincipalFactory claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task<ClaimsPrincipal> Handle(GetUserClaimsByIdRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                throw new MicroserviceException();
            }
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new MicroserviceUserNotFoundException();
            }

            var defaultClaims = await _claimsFactory.CreateAsync(user);
            return defaultClaims;
        }
    }
}
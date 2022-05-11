using Calabonga.Microservice.IdentityModule.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Identity.Queries
{
    public record GetUserByIdRequest(string UserId) : IRequest<ApplicationUser>;

    public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserByIdRequestHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;

        public async Task<ApplicationUser> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            return await _userManager.FindByIdAsync(request.UserId);
        }
    }
}
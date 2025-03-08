using Calabonga.Utils.Extensions;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Microservice.IdentityModule.Web.Application.Messaging.ProfileMessages.Queries;

/// <summary>
/// Request: Returns user roles 
/// </summary>
public static class GetProfile
{
    public record Request : IRequest<string>;

    public class Handler(ILogger<Handler> logger, IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<Request, string>
    {
        public Task<string> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = httpContextAccessor.HttpContext!.User;
            var claims = user.FindAll(x => x.Type == ClaimTypes.Role);
            var roles = ClaimsHelper.GetValues<string>(new ClaimsIdentity(claims), ClaimTypes.Role);
            var message = $"Current user ({user.Identity!.Name}) have following roles: {string.Join('|', roles)}";
            logger.LogInformation(message);
            return Task.FromResult(message);
        }
    }
}

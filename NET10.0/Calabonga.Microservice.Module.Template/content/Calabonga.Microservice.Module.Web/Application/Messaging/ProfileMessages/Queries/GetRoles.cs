
using Calabonga.Utils.Extensions;
using Mediator;
using System.Security.Claims;

namespace Calabonga.Microservice.Module.Web.Application.Messaging.ProfileMessages.Queries;

/// <summary>
/// Request: Returns user roles 
/// </summary>
public static class GetProfile
{
    public record Request : IRequest<string>;

    public class Handler(ILogger<Handler> logger, IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<Request, string>
    {
        public ValueTask<string> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = httpContextAccessor.HttpContext!.User;
            var claims = user.FindAll(x => x.Type == "role");
            var roles = ClaimsHelper.GetValues<string>(new ClaimsIdentity(claims), "role");
            var message = $"Current user ({user.Identity!.Name}) have following roles: {string.Join('|', roles)}";
            logger.LogInformation(message);
            return ValueTask.FromResult(message);
        }
    }
}

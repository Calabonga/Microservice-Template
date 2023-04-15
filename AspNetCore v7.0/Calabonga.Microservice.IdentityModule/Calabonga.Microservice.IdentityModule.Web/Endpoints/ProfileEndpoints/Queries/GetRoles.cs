using Calabonga.Microservices.Core;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints.ProfileEndpoints.Queries;

/// <summary>
/// Request: Returns user roles 
/// </summary>
public record GetRolesRequest : IRequest<string>;

public class GetRolesRequestHandler : IRequestHandler<GetRolesRequest, string>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<GetRolesRequestHandler> _logger;

    public GetRolesRequestHandler(IHttpContextAccessor httpContextAccessor, ILogger<GetRolesRequestHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public Task<string> Handle(GetRolesRequest request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext!.User;
        var roles = ClaimsHelper.GetValues<string>((ClaimsIdentity)user.Identity!, "role");
        _logger.LogInformation("Current user {@UserName} have following roles {@Roles}", user.Identity.Name, roles);
        return Task.FromResult($"Current user ({user.Identity!.Name}) have following roles: {string.Join("|", roles)}");
    }
}
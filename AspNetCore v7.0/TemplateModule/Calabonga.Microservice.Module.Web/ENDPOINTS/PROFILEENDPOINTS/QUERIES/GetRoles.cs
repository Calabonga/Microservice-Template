using Calabonga.Microservices.Core;
using MediatR;
using System.Security.Claims;

namespace $safeprojectname$.Endpoints.ProfileEndpoints.Queries;

/// <summary>
/// Request: Returns user roles 
/// </summary>
public record GetRolesRequest : IRequest<string>;

public class GetRolesRequestHandler : IRequestHandler<GetRolesRequest, string>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetRolesRequestHandler(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    public Task<string> Handle(GetRolesRequest request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext!.User;
        var roles = ClaimsHelper.GetValues<string>((ClaimsIdentity)user.Identity!, ClaimTypes.Role);
        return Task.FromResult($"Current user ({user.Identity!.Name}) have following roles: {string.Join("|", roles)}");
    }
}
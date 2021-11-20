using $safeprojectname$.Definitions.Identity.Queries;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using MediatR;

namespace $safeprojectname$.Definitions.Identity;

/// <summary>
/// Custom implementation of the IProfileService
/// </summary>
public class IdentityProfileService : IProfileService
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Identity Profile Service
    /// </summary>
    /// <param name="accountService"></param>
    public IdentityProfileService(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// This method is called whenever claims about the user are requested (e.g. during token creation or via the user info endpoint)
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var identifier = context.Subject.GetSubjectId();
        var profile = await _mediator.Send(new GetUserClaimsByIdRequest(identifier));
        context.IssuedClaims = profile.Claims.ToList();
    }

    /// <summary>
    /// Returns IsActive (IdentityServer4)
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task IsActiveAsync(IsActiveContext context)
    {
        var identifier = context.Subject.GetSubjectId();
        var user = await _mediator.Send(new GetUserByIdRequest(identifier));
        context.IsActive = user != null;

    }

}
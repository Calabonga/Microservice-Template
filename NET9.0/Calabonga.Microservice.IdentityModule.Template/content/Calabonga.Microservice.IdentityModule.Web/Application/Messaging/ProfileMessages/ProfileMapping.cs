using Calabonga.Microservice.IdentityModule.Infrastructure;
using Calabonga.Microservice.IdentityModule.Web.Application.Messaging.ProfileMessages.ViewModels;
using Calabonga.Utils.Extensions;
using System.Security.Claims;

namespace Calabonga.Microservice.IdentityModule.Web.Application.Messaging.ProfileMessages;

/// <summary>
/// Mappings for Profile entity
/// </summary>
public static class ProfileMapping
{
    /// <summary>
    /// Creates an ApplicationUser
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static ApplicationUser? MapToApplicationUser(this RegisterViewModel? source)
    {
        if (source is null)
        {
            return null;
        }

        return new ApplicationUser
        {
            UserName = source.Email,
            Email = source.Email,
            EmailConfirmed = true,
            FirstName = source.FirstName,
            LastName = source.LastName,
            PhoneNumberConfirmed = true
        };
    }

    /// <summary>
    /// Create ApplicationUser Profile
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static ApplicationUserProfile? MapToApplicationUserProfile(this RegisterViewModel? source)
    {
        if (source is null)
        {
            return null;
        }

        return new ApplicationUserProfile();
    }

    public static UserProfileViewModel? MapToProfileViewModel(this ClaimsPrincipal? source)
    {
        if (source?.Identity is null)
        {
            return null;
        }

        return ((ClaimsIdentity)source.Identity).InternalMapToProfileViewModel();

    }

    private static UserProfileViewModel? InternalMapToProfileViewModel(this ClaimsIdentity? source)
    {
        if (source is null)
        {
            return null;
        }

        return new UserProfileViewModel
        {
            Id = ClaimsHelper.GetValue<Guid>(source, ClaimTypes.NameIdentifier),
            FirstName = ClaimsHelper.GetValue<string>(source, ClaimTypes.GivenName),
            LastName = ClaimsHelper.GetValue<string>(source, ClaimTypes.Surname),
            Email = ClaimsHelper.GetValue<string>(source, ClaimTypes.Name),
            Roles = ClaimsHelper.GetValues<string>(source, ClaimTypes.Role),
            PhoneNumber = ClaimsHelper.GetValue<string>(source, ClaimTypes.MobilePhone),
            PositionName = ClaimsHelper.GetValue<string>(source, ClaimTypes.Actor)
        };
    }
}

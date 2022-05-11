using AutoMapper;
using $safeprojectname$.Endpoints.ProfileEndpoints.ViewModels;
using Calabonga.Microservices.Core;
using System.Security.Claims;

namespace $safeprojectname$.Endpoints.ProfileEndpoints;

/// <summary>
/// Mapper Configuration for entity ApplicationUser
/// </summary>
public class ProfilesMapperConfiguration : Profile
{
    /// <inheritdoc />
    public ProfilesMapperConfiguration()
        => CreateMap<ClaimsIdentity, UserProfileViewModel>()
            .ForMember(x => x.Id, o => o.MapFrom(claims => ClaimsHelper.GetValue<Guid>(claims, ClaimTypes.Name)))
            .ForMember(x => x.PositionName, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.Actor)))
            .ForMember(x => x.FirstName, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.GivenName)))
            .ForMember(x => x.LastName, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.Surname)))
            .ForMember(x => x.Roles, o => o.MapFrom(claims => ClaimsHelper.GetValues<string>(claims, ClaimTypes.Role)))
            .ForMember(x => x.Email, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.Name)))
            .ForMember(x => x.PhoneNumber, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.MobilePhone)));
}
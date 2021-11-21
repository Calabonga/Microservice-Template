using AutoMapper;
using Calabonga.Microservice.IdentityModule.Infrastructure;
using Calabonga.Microservice.IdentityModule.Web.Endpoints.ProfileEndpoints.ViewModels;
using Calabonga.Microservices.Core;
using IdentityModel;
using System.Security.Claims;

namespace Calabonga.Microservice.IdentityModule.Web.Endpoints.ProfileEndpoints
{
    /// <summary>
    /// Mapper Configuration for entity ApplicationUser
    /// </summary>
    public class ProfilesMapperConfiguration : Profile
    {
        /// <inheritdoc />
        public ProfilesMapperConfiguration()
        {
            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(x => x.UserName, o => o.MapFrom(p => p.Email))
                .ForMember(x => x.Email, o => o.MapFrom(p => p.Email))
                .ForMember(x => x.EmailConfirmed, o => o.MapFrom(src => true))
                .ForMember(x => x.FirstName, o => o.MapFrom(p => p.FirstName))
                .ForMember(x => x.LastName, o => o.MapFrom(p => p.LastName))
                .ForMember(x => x.PhoneNumberConfirmed, o => o.MapFrom(src => true))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<ClaimsIdentity, UserProfileViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(claims => ClaimsHelper.GetValue<Guid>(claims, JwtClaimTypes.Subject)))
                .ForMember(x => x.PositionName, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.Actor)))
                .ForMember(x => x.FirstName, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, JwtClaimTypes.GivenName)))
                .ForMember(x => x.LastName, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, ClaimTypes.Surname)))
                .ForMember(x => x.Roles, o => o.MapFrom(claims => ClaimsHelper.GetValues<string>(claims, JwtClaimTypes.Role)))
                .ForMember(x => x.Email, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, JwtClaimTypes.Name)))
                .ForMember(x => x.PhoneNumber, o => o.MapFrom(claims => ClaimsHelper.GetValue<string>(claims, JwtClaimTypes.PhoneNumber)))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }

    /// <summary>
    /// Mapper Configuration for entity Person
    /// </summary>
    public class ApplicationUserProfileMapperConfiguration : Profile
    {
        /// <inheritdoc />
        public ApplicationUserProfileMapperConfiguration()
            => CreateMap<RegisterViewModel, ApplicationUserProfile>().ForAllOtherMembers(x => x.Ignore());
    }
}
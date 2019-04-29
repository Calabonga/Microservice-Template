using System;
using System.Security.Claims;
using $ext_safeprojectname$.Data;
using $safeprojectname$.Infrastructure.Auth;
using $safeprojectname$.Infrastructure.Mappers.Base;
using $safeprojectname$.Infrastructure.ViewModels.AccountViewModels;

namespace $safeprojectname$.Infrastructure.Mappers
{
    /// <summary>
    /// Mapper Configuration for entity ApplicationUser
    /// </summary>
    public class ApplicationUserMapperConfiguration : MapperConfigurationBase
    {
        /// <inheritdoc />
        public ApplicationUserMapperConfiguration()
        {
            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(x => x.UserName, o => o.MapFrom(p => p.Email))
                .ForMember(x => x.Email, o => o.MapFrom(p => p.Email))
                .ForMember(x => x.EmailConfirmed, o => o.MapFrom(src => true))
                .ForMember(x => x.FirstName, o => o.MapFrom(p => p.FirstName))
                .ForMember(x => x.LastName, o => o.MapFrom(p => p.LastName))
                .ForMember(x => x.PhoneNumberConfirmed, o => o.MapFrom(src => true))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}

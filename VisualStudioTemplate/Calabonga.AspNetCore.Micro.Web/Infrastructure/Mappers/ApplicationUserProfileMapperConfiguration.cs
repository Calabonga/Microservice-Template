using Calabonga.AspNetCore.Micro.Data;
using $safeprojectname$.Infrastructure.Mappers.Base;
using $safeprojectname$.Infrastructure.ViewModels.AccountViewModels;

namespace $safeprojectname$.Infrastructure.Mappers
{
    /// <summary>
    /// Mapper Configuration for entity Person
    /// </summary>
    public class ApplicationUserProfileMapperConfiguration : MapperConfigurationBase
    {
        /// <inheritdoc />
        public ApplicationUserProfileMapperConfiguration()
        {
            CreateMap<RegisterViewModel, ApplicationUserProfile>()
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
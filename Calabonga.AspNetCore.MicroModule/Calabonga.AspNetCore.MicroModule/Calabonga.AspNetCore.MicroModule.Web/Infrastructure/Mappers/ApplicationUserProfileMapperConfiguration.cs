using Calabonga.AspNetCore.MicroModule.Data;
using Calabonga.AspNetCore.MicroModule.Web.Infrastructure.Mappers.Base;
using Calabonga.AspNetCore.MicroModule.Web.Infrastructure.ViewModels.AccountViewModels;

namespace Calabonga.AspNetCore.MicroModule.Web.Infrastructure.Mappers
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
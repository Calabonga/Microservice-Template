using Calabonga.AspNetCore.Micro.Data;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.Mappers.Base;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.ViewModels.AccountViewModels;

namespace Calabonga.AspNetCore.Micro.Web.Infrastructure.Mappers
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
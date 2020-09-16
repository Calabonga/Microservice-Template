using Calabonga.Microservice.IdentityModule.Data;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Mappers.Base;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.ViewModels.AccountViewModels;
using Calabonga.Microservice.IdentityModule.Web.ViewModels.AccountViewModels;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Mappers
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
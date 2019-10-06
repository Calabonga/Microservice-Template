using $safeprojectname$.ModelConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace $safeprojectname$.ModelConfigurations
{
    /// <summary>
    /// Entity Type Configuration for entity <see cref="ApplicationUserProfile"/>
    /// </summary>

    public class ApplicationUserProfileModelConfiguration : AuditableModelConfigurationBase<ApplicationUserProfile>
    {
        protected override void AddBuilder(EntityTypeBuilder<ApplicationUserProfile> builder)
        {
            builder.HasMany(x => x.Permissions);

            builder.HasOne(x => x.ApplicationUser)
                .WithOne(x => x.ApplicationUserProfile)
                .HasForeignKey<ApplicationUser>(x => x.ApplicationUserProfileId);
        }

        protected override string TableName()
        {
            return "ApplicationUserProfiles";
        }
    }
}
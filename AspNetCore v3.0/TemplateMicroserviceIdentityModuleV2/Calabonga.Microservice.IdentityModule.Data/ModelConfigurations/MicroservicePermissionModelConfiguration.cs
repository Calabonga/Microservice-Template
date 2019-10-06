using $safeprojectname$.ModelConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace $safeprojectname$.ModelConfigurations
{
    /// <summary>
    /// Entity Type Configuration for entity <see cref="MicroservicePermission"/>
    /// </summary>

    public class MicroservicePermissionModelConfiguration : AuditableModelConfigurationBase<MicroservicePermission>
    {
        protected override void AddBuilder(EntityTypeBuilder<MicroservicePermission> builder)
        {
            builder.Property(x => x.PolicyName).HasMaxLength(64).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1024);
            builder.Property(x => x.ApplicationUserProfileId).IsRequired();

            builder.HasOne(x => x.ApplicationUserProfile).WithMany(x => x.Permissions);
        }

        protected override string TableName()
        {
            return "MicroservicePermissions";
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace $safeprojectname$.ModelConfigurations
{
    /// <summary>
    /// Entity Type Configuration for entity <see cref="ApplicationUser"/>
    /// </summary>

    public class ApplicationUserModelConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {

        /// <summary>
        ///     Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.LastName).HasMaxLength(128).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(128).IsRequired();
            builder.Property(x => x.ApplicationUserProfileId).IsRequired(false);

            builder.HasOne(x => x.ApplicationUserProfile);
        }
    }
}
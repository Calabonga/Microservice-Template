using Calabonga.AspNetCore.Micro.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calabonga.AspNetCore.Micro.Data.ModelConfigurations.Base
{
    /// <summary>
    /// Audit-able Model Configuration base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IdentityModelConfigurationBase<T> : IEntityTypeConfiguration<T> where T : Identity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(TableName());
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            AddBuilder(builder);
        }

        /// <summary>
        /// Add custom properties for your entity
        /// </summary>
        /// <param name="builder"></param>
        protected abstract void AddBuilder(EntityTypeBuilder<T> builder);

        /// <summary>
        /// Table name
        /// </summary>
        /// <returns></returns>
        protected abstract string TableName();
    }
}
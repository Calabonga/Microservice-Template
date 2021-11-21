using Calabonga.EntityFrameworkCore.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Calabonga.Microservice.IdentityModule.Data.ModelConfigurations.Base
{
    /// <summary>
    /// Audit-able Model Configuration base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AuditableModelConfigurationBase<T> : IEntityTypeConfiguration<T> where T : Auditable
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(TableName());
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            // audit
            builder.Property(x => x.CreatedAt).IsRequired().HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc)).IsRequired();
            builder.Property(x => x.CreatedBy).HasMaxLength(256).IsRequired();
            builder.Property(x => x.UpdatedAt).HasConversion(v => v.Value, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            builder.Property(x => x.UpdatedBy).HasMaxLength(256);

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
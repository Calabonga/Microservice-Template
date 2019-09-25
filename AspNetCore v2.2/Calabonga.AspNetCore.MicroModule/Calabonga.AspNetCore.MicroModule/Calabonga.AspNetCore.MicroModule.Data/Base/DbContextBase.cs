using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Calabonga.AspNetCore.MicroModule.Models.Base;
using Calabonga.EntityFrameworkCore.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Calabonga.AspNetCore.MicroModule.Data.Base
{
    /// <summary>
    /// Base DbContext with predefined configuration
    /// </summary>
    public abstract class DbContextBase : DbContext
    {
        private const string DefaultUserName = "Anonymous";

        protected DbContextBase(DbContextOptions options) : base(options)
        {
            LastSaveChangesResult = new SaveChangesResult();
        }

        public SaveChangesResult LastSaveChangesResult { get; }


        /// <summary>
        ///     Asynchronously saves all changes made in this context to the database.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">
        ///     Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
        ///     been sent successfully to the database.
        /// </param>
        /// <remarks>
        ///     <para>
        ///         This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        ///         changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        ///     <para>
        ///         Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///         that any asynchronous operations have completed before calling another method on this context.
        ///     </para>
        /// </remarks>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                DbSaveChanges();
                return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            catch (Exception exception)
            {
                LastSaveChangesResult.Exception = exception;
                return Task.FromResult(0);
            }
        }


        /// <summary>
        ///     Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">
        ///     Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
        ///     been sent successfully to the database.
        /// </param>
        /// <remarks>
        ///     This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        ///     changes to entity instances before saving to the underlying database. This can be disabled via
        ///     <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        /// </remarks>
        /// <returns>
        ///     The number of state entries written to the database.
        /// </returns>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            try
            {
                DbSaveChanges();
                return base.SaveChanges(acceptAllChangesOnSuccess);
            }
            catch (Exception exception)
            {
                LastSaveChangesResult.Exception = exception;
                return 0;
            }
        }

        public override int SaveChanges()
        {
            try
            {
                DbSaveChanges();
                return base.SaveChanges();
            }
            catch (Exception exception)
            {
                LastSaveChangesResult.Exception = exception;
                return 0;
            }
        }

        /// <summary>
        ///     Asynchronously saves all changes made in this context to the database.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        ///         changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        ///     <para>
        ///         Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///         that any asynchronous operations have completed before calling another method on this context.
        ///     </para>
        /// </remarks>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                DbSaveChanges();
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                LastSaveChangesResult.Exception = exception;
                return Task.FromResult(0);
            }
        }

        private void DbSaveChanges()
        {
            var createdEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            foreach (var entry in createdEntries)
            {
                if (!(entry.Entity is IAuditable))
                {
                    continue;
                }

                var creationDate = DateTime.Now.ToUniversalTime();
                var userName = entry.Property("CreatedBy").CurrentValue == null
                    ? DefaultUserName
                    : entry.Property("CreatedBy").CurrentValue;
                var updatedAt = entry.Property("UpdatedAt").CurrentValue;
                var createdAt = entry.Property("CreatedAt").CurrentValue;
                if (createdAt != null)
                {
                    if (DateTime.Parse(createdAt.ToString()).Year > 1970)
                    {
                        entry.Property("CreatedAt").CurrentValue = ((DateTime)createdAt).ToUniversalTime();
                    }
                    else
                    {
                        entry.Property("CreatedAt").CurrentValue = creationDate;
                    }
                }
                else
                {
                    entry.Property("CreatedAt").CurrentValue = creationDate;
                }

                if (updatedAt != null)
                {
                    if (DateTime.Parse(updatedAt.ToString()).Year > 1970)
                    {
                        entry.Property("UpdatedAt").CurrentValue = ((DateTime)updatedAt).ToUniversalTime();
                    }
                    else
                    {
                        entry.Property("UpdatedAt").CurrentValue = creationDate;
                    }
                }
                else
                {
                    entry.Property("UpdatedAt").CurrentValue = creationDate;
                }

                entry.Property("CreatedBy").CurrentValue = userName;
                entry.Property("UpdatedBy").CurrentValue = userName;

                LastSaveChangesResult.AddMessage($"ChangeTracker has new entities: {entry.Entity.GetType()}");
            }

            var updatedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            foreach (var entry in updatedEntries)
            {
                if (entry.Entity is IAuditable)
                {
                    var userName = entry.Property("UpdatedBy").CurrentValue == null
                        ? DefaultUserName
                        : entry.Property("UpdatedBy").CurrentValue;
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now.ToUniversalTime();
                    entry.Property("UpdatedBy").CurrentValue = userName;
                }

                LastSaveChangesResult.AddMessage($"ChangeTracker has modified entities: {entry.Entity.GetType()}");
            }
        }

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">
        /// The builder being used to construct the model for this context.
        /// </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var applyGenericMethod = typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public).First(x => x.Name == "ApplyConfiguration");
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters))
            {
                foreach (var item in type.GetInterfaces())
                {
                    if (!item.IsConstructedGenericType || item.GetGenericTypeDefinition() != typeof(IEntityTypeConfiguration<>))
                    {
                        continue;
                    }

                    var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(item.GenericTypeArguments[0]);
                    applyConcreteMethod.Invoke(builder, new[] { Activator.CreateInstance(type) });
                    break;
                }
            }

            builder.EnableAutoHistory(2048);
        }
    }
}
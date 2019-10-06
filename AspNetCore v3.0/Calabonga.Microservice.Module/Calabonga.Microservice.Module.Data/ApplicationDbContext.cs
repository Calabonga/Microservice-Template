using Calabonga.Microservice.Module.Data.Base;
using Calabonga.Microservice.Module.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Microservice.Module.Data
{
    /// <summary>
    /// Database for application
    /// </summary>
    public class ApplicationDbContext : DbContextBase<ApplicationDbContext>, IApplicationDbContext
    {
        /// <inheritdoc />
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region System

        public DbSet<Log> Logs { get; set; }

        #endregion
    }
}
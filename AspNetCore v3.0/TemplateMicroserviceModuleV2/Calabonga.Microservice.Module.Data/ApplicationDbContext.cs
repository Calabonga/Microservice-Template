using $safeprojectname$.Base;
using $ext_projectname$.Entities;
using Microsoft.EntityFrameworkCore;

namespace $safeprojectname$
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
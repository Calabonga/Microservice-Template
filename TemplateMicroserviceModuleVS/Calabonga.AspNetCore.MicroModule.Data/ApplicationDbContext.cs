using $safeprojectname$.Base;
using $ext_safeprojectname$.Models;
using Microsoft.EntityFrameworkCore;

namespace $safeprojectname$
{
    /// <summary>
    /// Database for application
    /// </summary>
    public class ApplicationDbContext : DbContextBase, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        #region System

        public DbSet<Log> Logs { get; set; }

        #endregion
    }
}
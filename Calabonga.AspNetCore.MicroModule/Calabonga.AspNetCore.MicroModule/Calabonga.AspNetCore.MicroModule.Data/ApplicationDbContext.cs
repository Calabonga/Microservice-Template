using Calabonga.AspNetCore.MicroModule.Data.Base;
using Calabonga.AspNetCore.MicroModule.Models;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.AspNetCore.MicroModule.Data
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

        public DbSet<ApplicationUserProfile> Profiles { get; set; }

        #endregion
    }
}
using $safeprojectname$.Base;
using Calabonga.Microservice.Module.Entities;
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

        ///// <inheritdoc />
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .AddJsonFile($"appsettings.{environment}.json", optional: true)
        //        .AddEnvironmentVariables()
        //        .Build();

        //    optionsBuilder.UseSqlServer(configuration.GetConnectionString(nameof(ApplicationDbContext)));
        //}
    }
}
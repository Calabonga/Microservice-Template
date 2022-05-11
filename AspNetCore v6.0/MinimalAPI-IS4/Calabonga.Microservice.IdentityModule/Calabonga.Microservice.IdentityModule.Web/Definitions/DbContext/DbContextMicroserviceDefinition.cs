using Calabonga.Microservice.IdentityModule.Infrastructure;
using Calabonga.Microservice.IdentityModule.Web.Definitions.Base;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.DbContext
{
    /// <summary>
    /// ASP.NET Core services registration and configurations
    /// </summary>
    public class DbContextMicroserviceDefinition : MicroserviceDefinition
    {
        /// <summary>
        /// Configure services for current microservice
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<ApplicationDbContext>(config =>
            {
                // UseInMemoryDatabase - This for demo purposes only!
                // Should uninstall package "Microsoft.EntityFrameworkCore.InMemory" and install what you need. 
                // For example: "Microsoft.EntityFrameworkCore.SqlServer"
                // uncomment line below to use UseSqlServer(). Don't forget setup connection string in appSettings.json 
                config.UseInMemoryDatabase("DEMO_PURPOSES_ONLY");
                // config.UseSqlServer(configuration.GetConnectionString(nameof(ApplicationDbContext)));
            });
    }
}
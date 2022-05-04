using Calabonga.AuthService.Infrastructure.DatabaseInitialization;
using Calabonga.AuthService.Web.Definitions.Base;

namespace Calabonga.AuthService.Web.Definitions.DataSeeding
{
    /// <summary>
    /// Seeding DbContext for default data for EntityFrameworkCore
    /// </summary>
    public class DataSeedingDefinition : AppDefinition
    {
        /// <summary>
        /// Configure application for current application
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
            => DatabaseInitializer.Seed(app.Services);
    }
}

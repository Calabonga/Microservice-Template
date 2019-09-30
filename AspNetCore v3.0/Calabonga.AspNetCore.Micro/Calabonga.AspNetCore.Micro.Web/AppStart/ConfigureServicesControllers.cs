using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.AspNetCore.Micro.Web.AppStart
{
    /// <summary>
    /// Configure controllers
    /// </summary>
    public static class ConfigureServicesControllers
    {
        /// <summary>
        /// Configure services
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }
    }
}

using Calabonga.Microservice.Module.Data;
using Calabonga.Microservice.Module.Web.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Microservice.Module.Web.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Registrations for both points: API and Scheduler
    /// </summary>
    public partial class DependencyContainer
    {
        /// <summary>
        /// Register 
        /// </summary>
        /// <param name="services"></param>
        public static void Common(IServiceCollection services)
        {
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            
            // services
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<ICacheService, CacheService>();

            // notifications
            Notifications(services);
        }
    }


}

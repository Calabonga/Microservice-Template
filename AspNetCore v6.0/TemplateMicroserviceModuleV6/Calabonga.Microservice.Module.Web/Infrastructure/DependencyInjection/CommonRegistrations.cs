using $ext_projectname$.Data;
using $safeprojectname$.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$.Infrastructure.DependencyInjection
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
        }
    }

    
}

using Calabonga.AuthService.Web.Definitions.Base;

namespace Calabonga.AuthService.Web.Definitions.Common
{
    /// <summary>
    /// AspNetCore common configuration
    /// </summary>
    public class CommonDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLocalization();
            services.AddHttpContextAccessor();
            services.AddResponseCaching();
            services.AddMemoryCache();
        }
        
        /// <summary>
        /// Configure application for current application
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
            => app.UseHttpsRedirection();
    }
}
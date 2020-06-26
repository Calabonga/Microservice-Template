using AutoMapper;
using Calabonga.Microservice.Module.Data;
using Calabonga.Microservice.Module.Web.Extensions;
using Calabonga.Microservice.Module.Web.Infrastructure.Settings;
using Calabonga.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Microservice.Module.Web.AppStart.ConfigureServices
{
    /// <summary>
    /// ASP.NET Core services registration and configurations
    /// </summary>
    public static class ConfigureServicesCommon
    {
        /// <summary>
        /// ConfigureServices Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<ApplicationDbContext>(config =>
            {
                // This for demo only.
                // Should uninstall package "Microsoft.EntityFrameworkCore.InMemory" and
                // uncomment line below to use UseSqlServer(). Don't forget setup connection string in appSettings.json 
                config.UseInMemoryDatabase("DEMO_PURPOSES_ONLY");

                //config.UseSqlServer(configuration.GetConnectionString(nameof(ApplicationDbContext)));
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddUnitOfWork<ApplicationDbContext>();
            services.AddMemoryCache();
            services.AddRouting();
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services.AddOptions();
            services.Configure<CurrentAppSettings>(configuration.GetSection(nameof(CurrentAppSettings)));
            services.Configure<MvcOptions>(options => options.UseRouteSlugify());
            services.AddLocalization();
            services.AddHttpContextAccessor();
            services.AddResponseCaching();
        }
    }
}
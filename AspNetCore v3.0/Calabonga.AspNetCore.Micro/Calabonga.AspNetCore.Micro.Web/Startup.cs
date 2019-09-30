using Calabonga.AspNetCore.Micro.Web.AppStart;
using Calabonga.AspNetCore.Micro.Web.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Calabonga.AspNetCore.Micro.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServicesBase.ConfigureServices(services, Configuration);
            ConfigureServicesSwagger.ConfigureServices(services, Configuration);
            ConfigureServicesCors.ConfigureServices(services, Configuration);
            ConfigureServicesControllers.ConfigureServices(services);

            DependencyContainer.Common(services);
            DependencyContainer.Validators(services);
            DependencyContainer.ViewModelFactories(services);
            DependencyContainer.EntityManagers(services);
            DependencyContainer.EntityServices(services);
            DependencyContainer.Repositories(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="mapper"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AutoMapper.IConfigurationProvider mapper, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                mapper.AssertConfigurationIsValid();
            }
            else
            {
                mapper.CompileMappings();
            }
            
            ConfigureApplication.Configure(app, env);
        }
    }
}

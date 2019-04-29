using $safeprojectname$.AppStart;
using $safeprojectname$.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace $safeprojectname$
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
            ConfigureServicesBase.Configure(services, Configuration);
            ConfigureServicesSwagger.Configure(services, Configuration);
            ConfigureServicesCors.Configure(services, Configuration);
            ConfigureServicesEmailKit.Configure(services, Configuration);

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AutoMapper.IConfigurationProvider mapper, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                mapper.AssertConfigurationIsValid();
            }
            else
            {
                mapper.CompileMappings();
            }

            ConfigureApplication.Configure(app, env, loggerFactory);
        }
    }
}

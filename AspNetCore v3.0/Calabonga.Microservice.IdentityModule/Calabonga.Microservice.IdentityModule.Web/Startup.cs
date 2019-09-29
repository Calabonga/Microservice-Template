using Calabonga.Microservice.IdentityModule.Web.AppStart;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.DependencyInjection;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Mappers.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Calabonga.Microservice.IdentityModule.Web
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

            DependencyContainer.Common(services);
            DependencyContainer.Validators(services);
            DependencyContainer.ViewModelFactories(services);
            DependencyContainer.EntityManagers(services);
            DependencyContainer.EntityServices(services);
            DependencyContainer.Repositories(services);

            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AutoMapper.IConfigurationProvider mapper )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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

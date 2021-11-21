using Calabonga.Microservice.IdentityModule.Web.Definitions.Base;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Mapping
{
    /// <summary>
    /// Register Automapper as MicroserviceDefinition
    /// </summary>
    public class AutomapperMicroserviceDefinition : MicroserviceDefinition
    {
        /// <summary>
        /// Configure services for current microservice
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
            => services.AddAutoMapper(typeof(Program));

        /// <summary>
        /// Configure application for current microservice
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public override void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var mapper = app.ApplicationServices.GetRequiredService<AutoMapper.IConfigurationProvider>();
            if (env.IsDevelopment())
            {
                // validate Mapper Configuration
                mapper.AssertConfigurationIsValid();
            }
            else
            {
                mapper.CompileMappings();
            }
        }
    }
}
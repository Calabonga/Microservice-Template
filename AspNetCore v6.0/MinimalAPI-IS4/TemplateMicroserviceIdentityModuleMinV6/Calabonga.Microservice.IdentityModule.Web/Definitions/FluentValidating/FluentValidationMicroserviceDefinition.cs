using $safeprojectname$.Definitions.Base;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace $safeprojectname$.Definitions.FluentValidating
{
    /// <summary>
    /// FluentValidation registration as MicroserviceDefinition
    /// </summary>
    public class FluentValidationMicroserviceDefinition : MicroserviceDefinition
    {
        /// <summary>
        /// Configure services for current microservice
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        }
    }
}
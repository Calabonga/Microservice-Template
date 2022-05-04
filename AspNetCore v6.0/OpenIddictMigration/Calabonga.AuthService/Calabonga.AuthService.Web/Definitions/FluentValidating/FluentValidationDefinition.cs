using Calabonga.AuthService.Web.Definitions.Base;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.AuthService.Web.Definitions.FluentValidating
{
    /// <summary>
    /// FluentValidation registration as Application definition
    /// </summary>
    public class FluentValidationDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current application
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
using Calabonga.Microservice.IdentityModule.Web.Application;
using Calabonga.Microservice.IdentityModule.Web.Definitions.Base;
using MediatR;
using System.Reflection;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Mediator
{
    /// <summary>
    /// Register Mediator as MicroserviceDefinition
    /// </summary>
    public class MediatorMicroserviceDefinition : MicroserviceDefinition
    {
        /// <summary>
        /// Configure services for current microservice
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            //services.AddScoped<IPipelineBehavior<PostEventItemRequest, OperationResult<EventItemViewModel>>, TransactionBehavior<PostEventItemRequest, OperationResult<EventItemViewModel>>>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
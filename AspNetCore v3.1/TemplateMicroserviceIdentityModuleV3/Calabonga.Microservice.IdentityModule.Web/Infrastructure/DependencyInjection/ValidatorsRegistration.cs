using System.Linq;
using System.Reflection;
using Calabonga.Microservices.Core.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Dependency Injection Registration
    /// </summary>
    public partial class DependencyContainer
    {
        /// <summary>
        /// Registration EntityValidators
        /// </summary>
        /// <param name="services"></param>
        public static void Validators(IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && !t.IsAbstract);
            foreach (var type in types)
            {
                foreach (var i in type.GetInterfaces())
                {
                    if (!i.IsGenericType || i.GetGenericTypeDefinition() != typeof(IEntityValidator<>))
                    {
                        continue;
                    }

                    var interfaceType = typeof(IEntityValidator<>).MakeGenericType(i.GetGenericArguments());
                    services.AddTransient(interfaceType, type);
                }
            }
        }
    }
    
}
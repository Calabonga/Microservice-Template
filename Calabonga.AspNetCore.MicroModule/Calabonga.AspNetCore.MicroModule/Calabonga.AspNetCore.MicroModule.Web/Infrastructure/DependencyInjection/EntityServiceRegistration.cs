using System.Linq;
using System.Reflection;
using Calabonga.AspNetCore.MicroModule.Web.Infrastructure.Services.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.AspNetCore.MicroModule.Web.Infrastructure.DependencyInjection
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
        public static void EntityServices(IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && !t.IsAbstract);
            foreach (var type in types)
            {
                foreach (var i in type.GetInterfaces())
                {
                    if (!i.IsGenericType || i.GetGenericTypeDefinition() != typeof(IEntityService<>))
                    {
                        continue;
                    }

                    var interfaceType = typeof(IEntityService<>).MakeGenericType(i.GetGenericArguments());
                    services.AddTransient(interfaceType, type);
                }
            }
        }
    }

}
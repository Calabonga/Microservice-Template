using System.Linq;
using System.Reflection;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Dependency Injection Registration
    /// </summary>
    public partial class DependencyContainer
    {
        /// <summary>
        /// Registration ViewModel factories
        /// </summary>
        /// <param name="services"></param>
        public static void ViewModelFactories(IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && !t.IsAbstract);
            foreach (var type in types)
            {
                foreach (var i in type.GetInterfaces())
                {
                    if (!i.IsGenericType || i.GetGenericTypeDefinition() != typeof(IViewModelFactory<,>))
                    {
                        continue;
                    }

                    var interfaceType = typeof(IViewModelFactory<,>).MakeGenericType(i.GetGenericArguments());
                    services.AddTransient(interfaceType, type);
                }
            }
        }
    }
}

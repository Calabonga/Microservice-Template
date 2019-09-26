using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Microservice.Module.Web.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Dependency Injection Registration
    /// </summary>
    public partial class DependencyContainer
    {
        /// <summary>
        /// Registration Repositories
        /// </summary>
        /// <param name="services"></param>
        public static void Repositories(IServiceCollection services)
        {
            // custom repositories
            // not yet created

            // default repositories
            var repositories = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name.EndsWith("Repository"))
                .ToList();

            if (!repositories.Any())
            {
                return;
            }

            foreach (var repository in repositories)
            {
                //if (typeof(IMetadataRepository).IsAssignableFrom(repository))
                //{
                //    services.AddScoped(typeof(IMetadataRepository), repository);
                //}
                services.AddScoped(repository);
            }
        }
    }
}

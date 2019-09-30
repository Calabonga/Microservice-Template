using Calabonga.AspNetCore.Micro.Data.DatabaseInitialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Calabonga.AspNetCore.Micro.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();
            using (var scope = webHost.Services.CreateScope())
            {
                DatabaseInitializer.Seed(scope.ServiceProvider);
            }

            webHost.Run();
        }

        /// <summary>
        /// Create web host
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}

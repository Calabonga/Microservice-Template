using $ext_safeprojectname$.Data.DatabaseInitialization;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();
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
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

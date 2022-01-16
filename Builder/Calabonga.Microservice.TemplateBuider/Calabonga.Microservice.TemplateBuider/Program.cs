using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Calabonga.Microservice.TemplateBuilder
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.File("Application.log")
                .CreateLogger();
            
            Log.Logger.Information("Microservice Template Builder v.1.0.0-beta2");

            var configFile = args.Any() ? args[0] : "appsettings.json";

            if (string.IsNullOrWhiteSpace(configFile))
            {
                Log.Logger.Information("appSettings.json file not provided.");
                //return;
            }
            Log.Logger.Information("Using application settings from: {0}", configFile);
            Log.Logger.Information("Building configuration from {0}", configFile);
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configFile, optional: false)
                .Build();

            var projectsTemplates = configuration.Get<MicroserviceTemplates>();

            if (projectsTemplates is null)
            {
                Log.Logger.Information("{0} file not provided or configuration wrong.", configFile);
                return;
            }

            await RunProcessAsync(projectsTemplates.TemplateOptions);
        }

        private static async Task RunProcessAsync(List<TemplateOptions> templates)
        {
            foreach (var projectsTemplate in templates)
            {
                Log.Logger.Information($"Processing templates for {projectsTemplate.ProjectName}");
                var templatePath = projectsTemplate.RootDirectoryPath;
                Log.Logger.Information($"Processing path: {templatePath}");

                var directory = Directory.Exists(templatePath) ? new DirectoryInfo(templatePath) : null;

                if (directory is null)
                {
                    Log.Logger.Information("No directory was found");
                    return;
                }

                var projects = directory.GetDirectories();
                if (!projects.Any())
                {
                    Log.Logger.Information("No projects folder were found");
                    return;
                }

                // prepare manager
                var loadService = new ReadWriteService();
                var replaceService = new ReplaceService();
                var fileService = new FileService();
                var manager = new DataManager(projectsTemplate, fileService, loadService, replaceService);

                // run build process
                await manager.BuildAsync();

                Log.Logger.Information("Work completed");
            }
        }
    }
}
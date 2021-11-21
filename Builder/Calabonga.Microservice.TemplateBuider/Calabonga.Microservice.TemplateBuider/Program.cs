using System;
using System.IO;
using System.Linq;
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


            if (!args.Any())
            {
                Log.Logger.Information("appSettings.json file not provided.");
                return;
            }
            Log.Logger.Information($"Using application settings from: {args[0]}");
            var configFile = args[0];
            Log.Logger.Information("Microservice Template Builder v.1.0.0-beta2");
            Log.Logger.Information($"Building configuration from {configFile}");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configFile, optional: true)
                .Build();

            var projectsTemplates = configuration.Get<MicroserviceTemplates>();

            foreach (var projectsTemplate in projectsTemplates.TemplateOptions)
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
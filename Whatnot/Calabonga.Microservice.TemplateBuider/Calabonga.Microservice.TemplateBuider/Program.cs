using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Calabonga.Microservice.TemplateBuilder
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Microservice Template Builder v.1.0.0-beta1");
            Console.WriteLine("Building configuration from aspsettings.json");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var projectsTemplates = configuration.Get<MicroserviceTemplates>();

            foreach (var projectsTemplate in projectsTemplates.TemplateOptions)
            {
                Console.WriteLine($"Processing templates for {projectsTemplate.ProjectName}");
                var templatePath = projectsTemplate.RootDirectoryPath;
                Console.WriteLine($"Processing path: {templatePath}");

                var directory = Directory.Exists(templatePath) ? new DirectoryInfo(templatePath) : null;

                if (directory is null)
                {
                    Console.WriteLine("No directory was found");
                    return;
                }

                var projects = directory.GetDirectories();
                if (!projects.Any())
                {
                    Console.WriteLine("No projects folder were found");
                    return;
                }

                // prepare manager
                var loadService = new ReadWriteService();
                var replaceService = new ReplaceService();
                var fileService = new FileService();
                var manager = new DataManager(projectsTemplate, fileService, loadService, replaceService);

                // run build process
                await manager.BuildAsync();

                Console.WriteLine("Work completed");
                Console.ReadLine();
            }
        }
    }
}
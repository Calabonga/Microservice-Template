// ---------------------------------------
// Name: Microservice Template
// Author: Calabonga (calabonga.net)
// Version: 2.0.4   
// Based on: ASP.NET Core 3.1
// Created Date: 2019-10-06
// Updated Date 2020-09-04
// ---------------------------------------

using System;
using Calabonga.Microservice.Module.Core;
using Calabonga.Microservice.Module.Data.DatabaseInitialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Calabonga.Microservice.Module.Web
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
            Console.Title = $"{AppData.ServiceName} v.{ThisAssembly.Git.SemVer.Major}.{ThisAssembly.Git.SemVer.Minor}.{ThisAssembly.Git.SemVer.Patch}";
            webHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}

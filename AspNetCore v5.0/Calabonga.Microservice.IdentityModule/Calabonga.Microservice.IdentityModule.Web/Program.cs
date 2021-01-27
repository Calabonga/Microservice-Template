// ---------------------------------------
// Name: Microservice Template for ASP.NET Core API
// Author: Calabonga © Calabonga SOFT
// Version: 5.0.1
// Based on: .NET 5.0.x
// Created Date: 2019-10-06
// Updated Date 2021-01-27
// ---------------------------------------
// Contacts
// ---------------------------------------
// Blog: https://www.calabonga.net
// GitHub: https://github.com/Calabonga
// YouTube: https://youtube.com/sergeicalabonga
// ---------------------------------------
// Description:
// ---------------------------------------
// This template implements Web API and IdentityServer functionality.
// Also, support two type Authentications: Cookie and Bearer.
// ---------------------------------------

using System;
using Calabonga.Microservice.IdentityModule.Data.DatabaseInitialization;
using Calabonga.Microservice.IdentityModule.Entities.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Calabonga.Microservice.IdentityModule.Web
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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

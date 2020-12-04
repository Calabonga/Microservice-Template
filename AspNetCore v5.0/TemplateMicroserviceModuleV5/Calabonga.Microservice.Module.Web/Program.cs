// ---------------------------------------
// Name: Microservice Template for ASP.NET Core API
// Author: Calabonga © Calabonga SOFT
// Version: 5.0.0
// Based on: .NET 5.0
// Created Date: 2019-10-06
// Updated Date 2020-12-04
// ---------------------------------------
// Contacts
// ---------------------------------------
// Blog: https://www.calabonga.net
// GitHub: https://github.com/Calabonga
// YouTube: https://youtube.com/sergeicalabonga
// ---------------------------------------
// Description:
// ---------------------------------------
// This template implements Web API functionality.
// ---------------------------------------


using System;
using $ext_projectname$.Core;
using $ext_projectname$.Data.DatabaseInitialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace $safeprojectname$
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

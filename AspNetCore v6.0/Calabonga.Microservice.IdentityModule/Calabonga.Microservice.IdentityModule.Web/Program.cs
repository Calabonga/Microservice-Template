﻿// ---------------------------------------
// Name: Microservice Template for ASP.NET Core API
// Author: Calabonga © 2005-2021 Calabonga SOFT
// Version: 6.0.0
// Based on: .NET 6.0.x
// Created Date: 2021-11-09
// Updated Date 2021-11-09
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
using Serilog;
using Serilog.Events;
using System.Threading.Tasks;

namespace Calabonga.Microservice.IdentityModule.Web;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        try
        {
            var webHost = CreateHostBuilder(args).Build();
            using (var scope = webHost.Services.CreateScope())
            {
                DatabaseInitializer.Seed(scope.ServiceProvider);
            }

            Console.Title = $"{AppData.ServiceName} v.{ThisAssembly.Git.SemVer.Major}.{ThisAssembly.Git.SemVer.Minor}.{ThisAssembly.Git.SemVer.Patch}";
            await webHost.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
// --------------------------------------------------------------------
// Name: Template for Micro service on ASP.NET Core API with
// OpenIddict (OAuth2.0)
// Author: Calabonga © 2005-2023 Calabonga SOFT
// Version: 7.0.4
// Based on: .NET 7.0.x
// Created Date: 2022-11-12 09:29
// Updated Date: 2023-04-15 08:17
// --------------------------------------------------------------------
// Contacts
// --------------------------------------------------------------------
// Blog: https://www.calabonga.net/blog/post/nimble-framework-7
// GitHub: https://github.com/Calabonga/Microservice-Template
// YouTube: https://youtube.com/sergeicalabonga
// DZen: https://dzen.ru/calabonga
// --------------------------------------------------------------------
// Description:
// --------------------------------------------------------------------
// Minimal API for NET7 used with Definitions.
// This template implements Web API and OpenIddict (OAuth2.0).
// functionality. Also, support two type of Authentication:
// Cookie and Bearer
// --------------------------------------------------------------------

using Calabonga.AspNetCore.AppDefinitions;
using Serilog;
using Serilog.Events;

try
{
    // configure logger (Serilog)
    Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

    // created builder
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    // adding definitions for application
    builder.Services.AddDefinitions(builder, typeof(Program));

    // create application
    var app = builder.Build();

    // using definition for application
    app.UseDefinitions();

    // using Serilog request logging
    app.UseSerilogRequestLogging();

    // start application
    app.Run();

    return 0;
}
catch (Exception ex)
{
    var type = ex.GetType().Name;
    if (type.Equals("HostAbortedException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Unhandled exception");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
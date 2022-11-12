// --------------------------------------------------------------------
// Name: Template for Micro service on ASP.NET Core API with
// OpenIddict (OAuth2.0)
// Author: Calabonga © 2005-2022 Calabonga SOFT
// Version: 7.0.0
// Based on: .NET 7.0.x
// Created Date: 2022-11-12 9:29:12 PM
// Updated Date: 2022-11-12 9:29:12 PM
// --------------------------------------------------------------------
// Contacts
// --------------------------------------------------------------------
// Blog: https://www.calabonga.net
// GitHub: https://github.com/Calabonga
// YouTube: https://youtube.com/sergeicalabonga
// Zen: https://zen.yandex.ru/calabonga
// --------------------------------------------------------------------
// Description:
// --------------------------------------------------------------------
// Minimal API for NET7
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
    builder.Host.UseSerilog();

    // adding definitions for application
    builder.Services.AddDefinitions(builder, typeof(Program));

    // create application
    var app = builder.Build();

    // using definition for application
    app.UseDefinitions();

    // start application
    app.Run();

    return 0;
}
catch (Exception ex)
{
    var type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
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
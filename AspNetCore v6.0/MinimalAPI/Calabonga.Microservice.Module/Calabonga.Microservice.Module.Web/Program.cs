// --------------------------------------------------------------------
// Name: Microservice Template for ASP.NET Core API without IdentityServer but using it
// Author: Calabonga © 2005-2022 Calabonga SOFT
// Version: 6.0.1
// Based on: .NET 6.0.x
// Created Date: 2021-11-09
// Updated Date 2022-01-16
// --------------------------------------------------------------------
// Contacts
// --------------------------------------------------------------------
// Blog: https://www.calabonga.net
// GitHub: https://github.com/Calabonga
// YouTube: https://youtube.com/sergeicalabonga
// --------------------------------------------------------------------
// Description:
// --------------------------------------------------------------------
// Minimal API for NET6 used. 
// This template implements Web API and IdentityServer functionality.
// Also, support two type Authentications: Cookie and Bearer.
// --------------------------------------------------------------------

using Calabonga.Microservice.Module.Web.Definitions.Base;
using Calabonga.Microservice.Module.Web.Endpoints.Base;
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

    builder.Services.AddEndpointsToMicroservice(builder, typeof(Program));
    builder.Services.AddMicroserviceDefinitions(builder, typeof(Program));

    // create application
    var app = builder.Build();
    app.UseEndpointsInMicroservice();
    app.UseMicroserviceDefinitions();

    // start application
    app.Run();

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



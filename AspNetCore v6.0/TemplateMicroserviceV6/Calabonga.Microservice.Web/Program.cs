using $safeprojectname$.Definitions.Base;
using $safeprojectname$.Endpoints.Base;
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



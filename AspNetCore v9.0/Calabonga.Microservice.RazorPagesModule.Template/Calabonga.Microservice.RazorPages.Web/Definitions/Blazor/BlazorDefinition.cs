using Calabonga.AspNetCore.AppDefinitions;

namespace Calabonga.Microservice.RazorPages.Web.Definitions.Blazor;

public class BlazorDefinition : AppDefinition
{
    public override void ConfigureServices(WebApplicationBuilder builder)
        => builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

    public override void ConfigureApplication(WebApplication app)
        => app.MapBlazorHub();
}

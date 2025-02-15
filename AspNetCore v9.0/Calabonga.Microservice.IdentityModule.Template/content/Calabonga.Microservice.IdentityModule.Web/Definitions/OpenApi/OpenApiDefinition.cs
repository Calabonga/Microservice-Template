using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Microservice.IdentityModule.Domain.Base;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.OpenApi;

/// <summary>
/// Swagger definition for application
/// </summary>
public class OpenApiDefinition : AppDefinition
{
    // -------------------------------------------------------
    // ATTENTION!
    // -------------------------------------------------------
    // If you use are git repository then you can uncomment line with "ThisAssembly" below for versioning by GIT possibilities.
    // Otherwise, you can change versions of your API by manually.
    // If you are not going to use git-versioning, do not forget install package "GitInfo" 
    // private const string AppVersion = $"{ThisAssembly.Git.SemVer.Major}.{ThisAssembly.Git.SemVer.Minor}.{ThisAssembly.Git.SemVer.Patch}";
    // -------------------------------------------------------

    public const string AppVersion = "9.0.3";

    private const string _openApiConfig = "/openapi/v1.json";

    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<OAuth2SecuritySchemeTransformer>();
        });
    }

    public override void ConfigureApplication(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        app.MapOpenApi();

        app.UseSwaggerUI(settings =>
        {
            settings.SwaggerEndpoint(_openApiConfig, $"{AppData.ServiceName} v.{AppVersion}");

            // ATTENTION!
            // If you use are git repository then you can uncomment line with "ThisAssembly" below for versioning by GIT possibilities.
            // settings.HeadContent = $"{ThisAssembly.Git.Branch.ToUpper()} {ThisAssembly.Git.Commit.ToUpper()}";

            settings.DocumentTitle = $"{AppData.ServiceName}";
            settings.DefaultModelExpandDepth(0);
            settings.DefaultModelRendering(ModelRendering.Model);
            settings.DefaultModelsExpandDepth(0);
            settings.DocExpansion(DocExpansion.None);
            settings.OAuthScopeSeparator(" ");
            settings.OAuthClientId("client-id-code");
            settings.OAuthClientSecret("client-secret-code");
            settings.DisplayRequestDuration();
            settings.OAuthUsePkce();
            settings.OAuthAppName(AppData.ServiceName);
        });
    }
}

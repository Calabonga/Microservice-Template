namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Swagger;

/// <summary>
/// Swagger definition for application
/// </summary>
public class SwaggerDefinition : AppDefinition
{
    // ATTENTION!
    // If you use are git repository then you can uncomment line with "ThisAssembly" below for versioning by GIT possibilities.
    // Otherwise, you can change versions of your API by manually.
    // If you are not going to use git-versioning, do not forget install package "GitInfo" 
    // private const string AppVersion = $"{ThisAssembly.Git.SemVer.Major}.{ThisAssembly.Git.SemVer.Minor}.{ThisAssembly.Git.SemVer.Patch}";
    public const string AppVersion = "8.0.4";

    private const string _swaggerConfig = "/swagger/v1/swagger.json";

    public override void ConfigureApplication(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        var url = app.Services.GetRequiredService<IConfiguration>().GetValue<string>("AuthServer:Url");

        app.UseSwagger();
        app.UseSwaggerUI(settings =>
        {
            settings.SwaggerEndpoint(_swaggerConfig, $"{AppData.ServiceName} v.{AppVersion}");

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
            settings.OAuthAppName(AppData.ServiceName);
            settings.OAuth2RedirectUrl($"{url}/swagger/oauth2-redirect.html");
        });
    }

    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = AppData.ServiceName,
                Version = AppVersion,
                Description = AppData.ServiceDescription
            });

            options.ResolveConflictingActions(x => x.First());

            var url = builder.Configuration.GetSection("AuthServer").GetValue<string>("Url");

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri($"{url}/connect/token", UriKind.Absolute),
                        AuthorizationUrl = new Uri($"{url}/connect/authorize", UriKind.Absolute),
                        Scopes = new Dictionary<string, string>
                        {
                            { "api", "Default scope" }
                        }
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        },
                        In = ParameterLocation.Cookie,
                        Type = SecuritySchemeType.OAuth2

                    },
                    new List<string>()
                }
            });
        });
    }
}
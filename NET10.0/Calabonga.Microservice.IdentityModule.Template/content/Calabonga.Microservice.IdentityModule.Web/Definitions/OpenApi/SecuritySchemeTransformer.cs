using Calabonga.Microservice.IdentityModule.Domain.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using OpenIddict.Server.AspNetCore;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.OpenApi;

internal sealed class SecuritySchemeTransformer : IOpenApiDocumentTransformer
{
    private readonly string? _authServerUrl;
    private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

    public SecuritySchemeTransformer(
        IAuthenticationSchemeProvider authenticationSchemeProvider,
        IConfiguration configuration)
    {
        _authenticationSchemeProvider = authenticationSchemeProvider;
        _authServerUrl = configuration.GetSection("AuthServer").GetValue<string>("Url");

        ArgumentNullException.ThrowIfNull(_authServerUrl);
    }

    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var authenticationSchemes = await _authenticationSchemeProvider.GetAllSchemesAsync();
        var schemes = authenticationSchemes as AuthenticationScheme[] ?? authenticationSchemes.ToArray();
        var securitySchemes = new Dictionary<string, IOpenApiSecurityScheme>();

        if (schemes.Any(x => x.Name == "Bearer"))
        {
            securitySchemes.Add("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // "bearer" refers to the header name here
                In = ParameterLocation.Header,
                BearerFormat = "Json Web Token"
            });
        }

        if (schemes.Any(x => x.Name == OpenIddictServerAspNetCoreDefaults.AuthenticationScheme))
        {
            securitySchemes.Add("OAuth2.0",
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{_authServerUrl}/connect/token", UriKind.Absolute),
                            AuthorizationUrl = new Uri($"{_authServerUrl}/connect/authorize", UriKind.Absolute),
                            Scopes = new Dictionary<string, string> { { "api", "Default scope" } }
                        }
                    }
                });
        }

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = securitySchemes;
        document.Info = new()
        {
            License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://mit-license.org/") },
            Title = AppData.ServiceName,
            Version = OpenApiDefinition.AppVersion,
            Description = AppData.ServiceDescription,
            Contact = new() { Name = "Sergei Calabonga", Url = new("https://www.calabonga.net"), }
        };
    }
}

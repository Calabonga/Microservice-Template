using Calabonga.Microservice.Module.Domain.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using OpenIddict.Server.AspNetCore;


namespace Calabonga.Microservice.Module.Web.Definitions.OpenApi;

/// <summary>
/// Swagger security configuration scheme
/// </summary>
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

        //// Add OAuth2 security scheme (Authorization Code flow only)
        //document.Components.SecuritySchemes.Add("oauth2", new OpenApiSecurityScheme
        //{
        //    Type = SecuritySchemeType.OAuth2,
        //    Flows = new OpenApiOAuthFlows
        //    {
        //        AuthorizationCode = new OpenApiOAuthFlow
        //        {
        //            AuthorizationUrl = new Uri("https://demo.duendesoftware.com/connect/authorize"),
        //            TokenUrl = new Uri("https://demo.duendesoftware.com/connect/token"),
        //            Scopes = new Dictionary<string, string>
        //            {
        //                { "api", "Access the Weather API" },
        //                { "openid", "Access the OpenID Connect user profile" },
        //                { "email", "Access the user's email address" },
        //                { "profile", "Access the user's profile" }
        //            }
        //        }
        //    }
        //});

        // Apply security requirement globally
        document.Security = [
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference("oauth2"),
                    ["api", "profile", "email", "openid"]
                }
            }
        ];

        // Set the host document for all elements
        // including the security scheme references
        document.SetReferenceHostDocument();
    }
}

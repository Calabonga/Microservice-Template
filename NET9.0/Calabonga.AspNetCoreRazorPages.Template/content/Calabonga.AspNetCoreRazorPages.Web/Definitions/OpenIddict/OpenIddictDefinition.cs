using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.AspNetCoreRazorPages.Infrastructure;
using OpenIddict.Client;
using System.Configuration;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Calabonga.AspNetCoreRazorPages.Web.Definitions.OpenIddict;

public class OpenIddictDefinition : AppDefinition
{
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddOpenIddict()
            // Register the OpenIddict core components.
            .AddCore(options =>
             {
                 // Configure OpenIddict to use the Entity Framework Core stores and models.
                 // Note: call ReplaceDefaultEntities() to replace the default entities.
                 options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>();
             })

             // Register the OpenIddict client components.
             .AddClient(options =>
             {
                 // Note: this sample only uses the authorization code flow,
                 // but you can enable the other flows if necessary.
                 options.AllowAuthorizationCodeFlow();

                 // Register the signing and encryption credentials used to protect
                 // sensitive data like the state tokens produced by OpenIddict.
                 options.AddDevelopmentEncryptionCertificate()
                     .AddDevelopmentSigningCertificate();


                 // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                 options.UseAspNetCore()
                     .EnableStatusCodePagesIntegration()
                     .EnableRedirectionEndpointPassthrough()
                     .EnablePostLogoutRedirectionEndpointPassthrough()
                     .DisableTransportSecurityRequirement();

                 // Register the System.Net.Http integration.
                 options.UseSystemNetHttp()
                     .SetProductInformation(typeof(Program).Assembly);

                 var url = builder.Configuration["AuthService:Url"];
                 var clientId = builder.Configuration["AuthService:ClientId"];
                 var clientSecret = builder.Configuration["AuthService:ClientSecret"];

                 if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                 {
                     throw new ConfigurationErrorsException("AuthService configuration not found");
                 }

                 options.AddRegistration(new OpenIddictClientRegistration
                 {
                     Issuer = new Uri(url, UriKind.Absolute),
                     ClientId = clientId,
                     ClientSecret = clientSecret,
                     Scopes = { Scopes.Email, Scopes.Profile, Scopes.OfflineAccess, Scopes.Roles },

                     // Note: to mitigate mix-up attacks, it's recommended to use a unique redirection endpoint
                     // URI per provider, unless all the registered providers support returning a special "iss"
                     // parameter containing their URL as part of authorization responses. For more information,
                     // see https://datatracker.ietf.org/doc/html/draft-ietf-oauth-security-topics#section-4.4.
                     RedirectUri = new Uri("callback/login/local", UriKind.Relative),
                     PostLogoutRedirectUri = new Uri("callback/logout/local", UriKind.Relative)
                 });

             });
    }
}

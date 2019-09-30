using System;
using System.Collections.Generic;
using System.Linq;
using Calabonga.AspNetCore.Micro.Web.AppStart.SwaggerFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Calabonga.AspNetCore.Micro.Web.AppStart
{
    /// <summary>
    /// Swagger configuration
    /// </summary>
    public static class ConfigureServicesSwagger
    {
        private const string AppTitle = "Microservice API";
        private const string AppVersion = "1.0.0-alpha-1";
        private const string SwaggerConfig = "/swagger/v1/swagger.json";
        private const string SwaggerUrl = "api/manual";

        /// <summary>
        /// Configure Swagger services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = AppTitle,
                    Version = AppVersion,
                    Description = "Microservice API documentation"
                });
                options.ResolveConflictingActions(x => x.First());
                //options.DescribeAllEnumsAsStrings();

                //var security = new Dictionary<string, IEnumerable<string>>
                //{
                //    { "Bearer", new string[] { } },
                //    { "oauth2", new string[] { } }
                //};
                //options.AddSecurityRequirement(security);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                var url = configuration.GetSection("IdentityServer").GetValue<string>("SwaggerUrl");

                //options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                //{
                //    Type = "oauth2",
                //    Flow = "password",
                //    TokenUrl = $"{url}/auth/connect/token",
                //    Scopes = new Dictionary<string, string>{
                //        { "api1", "API Default" }
                //    }
                //});

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(url, UriKind.Absolute),
                            Scopes = new Dictionary<string, string>
                            {
                                { "readAccess", "Access read operations" },
                                { "writeAccess", "Access write operations" }
                            }
                        }
                    }
                });

                options.DocumentFilter<LowercaseDocumentFilter>();
                options.OperationFilter<ApplySummariesOperationFilter>();
            });
        }

        /// <summary>
        /// Set up some properties for swagger UI
        /// </summary>
        /// <param name="settings"></param>
        public static void SwaggerSettings(SwaggerUIOptions settings)
        {
            settings.SwaggerEndpoint(SwaggerConfig, $"{AppTitle} v.{AppVersion}");
            settings.RoutePrefix = SwaggerUrl;
            settings.DocumentTitle = "API documentation";
            settings.DefaultModelExpandDepth(0);
            settings.DefaultModelRendering(ModelRendering.Model);
            settings.DefaultModelsExpandDepth(0);
            settings.DocExpansion(DocExpansion.None);
            settings.OAuthClientId("microservice1");
            settings.OAuthScopeSeparator(" ");
            settings.OAuthClientSecret("secret");
            settings.DisplayRequestDuration();
            settings.OAuthAppName("Micro service");
            settings.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        }
    }
}

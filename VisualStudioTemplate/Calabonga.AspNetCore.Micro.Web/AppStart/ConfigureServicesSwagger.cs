using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using $safeprojectname$.AppStart.SwaggerFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace $safeprojectname$.AppStart
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
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = AppTitle,
                    Version = AppVersion,
                    Description = "Microservice API documentation"
                });
                options.ResolveConflictingActions(x => x.First());
                options.DescribeAllEnumsAsStrings();

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } },
                    { "oauth2", new string[] { } }
                };
                options.AddSecurityRequirement(security);

                var url = configuration.GetSection("IdentityServer").GetValue<string>("SwaggerUrl");
                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "password",
                    TokenUrl = $"{url}/auth/connect/token",
                    Scopes = new Dictionary<string, string>
                {
                { "api1", "API Default" }
                }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.DocumentFilter<LowercaseDocumentFilter>();

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

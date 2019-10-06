using System;
using System.Collections.Generic;
using System.Linq;
using $safeprojectname$.AppStart.SwaggerFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace $safeprojectname$.AppStart.ConfigureServices
{
    /// <summary>
    /// Swagger configuration
    /// </summary>
    public static class ConfigureServicesSwagger
    {
        private const string AppTitle = "Microservice API";
        private const string AppVersion = "2.0.0-alpha-1";
        private const string SwaggerConfig = "/swagger/v1/swagger.json";
        private const string SwaggerUrl = "api/manual";

        /// <summary>
        /// ConfigureServices Swagger services
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
                    Description = "Microservice API module API documentation. This template based on ASP.NET Core 3.0."
                });
                options.ResolveConflictingActions(x => x.First());

                var url = configuration.GetSection("IdentityServer").GetValue<string>("SwaggerUrl");

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{url}/auth/connect/token", UriKind.Absolute),
                            Scopes = new Dictionary<string, string>
                            {
                                { "api1", "Default scope" }
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
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

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
            settings.DocumentTitle = "Microservice API";
            settings.DefaultModelExpandDepth(0);
            settings.DefaultModelRendering(ModelRendering.Model);
            settings.DefaultModelsExpandDepth(0);
            settings.DocExpansion(DocExpansion.None);
            settings.OAuthClientId("microservice1");
            settings.OAuthScopeSeparator(" ");
            settings.OAuthClientSecret("secret");
            settings.DisplayRequestDuration();
            settings.OAuthAppName("Microservice API module API documentation");
        }
    }
}

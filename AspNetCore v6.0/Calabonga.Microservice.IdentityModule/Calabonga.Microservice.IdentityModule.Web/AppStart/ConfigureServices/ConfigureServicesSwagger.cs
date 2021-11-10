using System;
using System.Collections.Generic;
using System.Linq;
using Calabonga.Microservice.IdentityModule.Web.AppStart.SwaggerFilters;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection.Metadata.Ecma335;

namespace Calabonga.Microservice.IdentityModule.Web.AppStart.ConfigureServices;

/// <summary>
/// Swagger configuration
/// </summary>
public static class ConfigureServicesSwagger
{
    private const string AppTitle = "Microservice API with IdentityServer4";
    private static readonly string AppVersion = $"{ThisAssembly.Git.SemVer.Major}.{ThisAssembly.Git.SemVer.Minor}.{ThisAssembly.Git.SemVer.Patch}";
    private const string SwaggerConfig = "/swagger/v1/swagger.json";

    /// <summary>
    /// ConfigureServices Swagger services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration) => services.AddSwaggerGen(options =>
    {

        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = AppTitle,
            Version = AppVersion,
            Description = "Microservice module API with IdentityServer4. This template based on .NET 6.0"
        });

        options.TagActionsBy(api =>
        {
            string tag;
            if (api.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                var attribute = descriptor.EndpointMetadata.OfType<SwaggerGroupAttribute>().FirstOrDefault();
                tag = attribute?.GroupName ?? descriptor.ControllerName;
            }
            else
            {
                tag = api.RelativePath!;
            }

            var tags = new List<string>();
            if (!string.IsNullOrEmpty(tag))
            {
                tags.Add(tag);
            }
            return tags;
        });

        options.ResolveConflictingActions(x => x.First());

        var url = configuration.GetSection("IdentityServer").GetValue<string>("Url");

        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                Password = new OpenApiOAuthFlow
                {
                    TokenUrl = new Uri($"{url}/connect/token", UriKind.Absolute),
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

    /// <summary>
    /// Set up some properties for swagger UI
    /// </summary>
    /// <param name="settings"></param>
    public static void SwaggerSettings(SwaggerUIOptions settings)
    {
        settings.SwaggerEndpoint(SwaggerConfig, $"{AppTitle} v.{AppVersion}");
        settings.HeadContent = $"{ThisAssembly.Git.Branch.ToUpper()} {ThisAssembly.Git.Commit.ToUpper()}";
        settings.DocumentTitle = $"{AppTitle}";
        settings.DefaultModelExpandDepth(0);
        settings.DefaultModelRendering(ModelRendering.Model);
        settings.DefaultModelsExpandDepth(0);
        settings.DocExpansion(DocExpansion.None);
        settings.OAuthClientId("microservice1");
        settings.OAuthScopeSeparator(" ");
        settings.OAuthClientSecret("secret");
        settings.DisplayRequestDuration();
        settings.OAuthAppName("Microservice module API with IdentityServer4");
    }
}
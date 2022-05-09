using Calabonga.Microservice.Module.Domain.Base;
using Calabonga.Microservice.Module.Web.Definitions.Base;
using Calabonga.Microservice.Module.Web.Definitions.OpenIddict;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Server.AspNetCore;
using System.Text;
using System.Text.Json;

namespace Calabonga.Microservice.Module.Web.Definitions.Identity;

/// <summary>
/// Authorization Policy registration
/// </summary>
public class AuthorizationDefinition : AppDefinition
{
    /// <summary>
    /// Configure services for current microservice
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration.GetSection("AuthServer").GetValue<string>("Url");

        services
            .AddAuthentication(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)
            .AddJwtBearer(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, "Bearer", options =>
            {
                options.SaveToken = true;
                options.Audience = AppData.ServiceName;
                options.Authority = url;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ClockSkew = new TimeSpan(0, 0, 30)
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        // Ensure we always have an error and error description.
                        if (string.IsNullOrEmpty(context.Error))
                        {
                            context.Error = "invalid_token";
                        }

                        if (string.IsNullOrEmpty(context.ErrorDescription))
                        {
                            context.ErrorDescription = "This request requires a valid JWT access token to be provided";
                        }

                        // Add some extra context for expired tokens.
                        if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                            context.Response.Headers.Add("x-token-expired", authenticationException?.Expires.ToString("o"));
                            context.ErrorDescription = $"The token expired on {authenticationException?.Expires:o}";
                        }

                        return context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            error = context.Error,
                            error_description = context.ErrorDescription
                        }));
                    }
                };
            });

        //.AddJwtBearer(options =>  {
        //    options.Authority = url;
        //    options.Audience = url;
        //    options.TokenValidationParameters = new TokenValidationParameters()
        //    {
        //        ClockSkew = new System.TimeSpan(0, 0, 30)
        //    };
        //    options.Events = new JwtBearerEvents
        //    {
        //        OnChallenge = context =>
        //        {
        //            context.HandleResponse();
        //            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //            context.Response.ContentType = "application/json";

        //            // Ensure we always have an error and error description.
        //            if (string.IsNullOrEmpty(context.Error))
        //            {
        //                context.Error = "invalid_token";
        //            }

        //            if (string.IsNullOrEmpty(context.ErrorDescription))
        //            {
        //                context.ErrorDescription = "This request requires a valid JWT access token to be provided";
        //            }

        //            // Add some extra context for expired tokens.
        //            if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
        //            {
        //                var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
        //                context.Response.Headers.Add("x-token-expired", authenticationException.Expires.ToString("o"));
        //                context.ErrorDescription = $"The token expired on {authenticationException.Expires.ToString("o")}";
        //            }

        //            return context.Response.WriteAsync(JsonSerializer.Serialize(new
        //            {
        //                error = context.Error,
        //                error_description = context.ErrorDescription
        //            }));
        //        }
        //    };
        //});



        services.AddAuthorization();

        services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
        services.AddSingleton<IAuthorizationHandler, AppPermissionHandler>();
    }

    /// <summary>
    /// Configure application for current microservice
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseCors(AppData.PolicyName);
        app.UseAuthentication();
        app.UseAuthorization();

        // registering UserIdentity helper as singleton
        UserIdentity.Instance.Configure(app.Services.GetService<IHttpContextAccessor>()!);
    }
}
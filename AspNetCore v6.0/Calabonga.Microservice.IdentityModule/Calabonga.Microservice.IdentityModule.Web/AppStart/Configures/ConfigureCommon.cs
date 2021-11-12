using Calabonga.Microservice.IdentityModule.Web.AppStart.ConfigureServices;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Auth;
using Calabonga.Microservice.IdentityModule.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Microservice.IdentityModule.Web.AppStart.Configures;

/// <summary>
/// Pipeline configuration
/// </summary>
public static class ConfigureCommon
{
    /// <summary>
    /// Configure pipeline
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="mapper"></param>
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, AutoMapper.IConfigurationProvider mapper)
    {
        if (env.IsDevelopment())
        {
            mapper.AssertConfigurationIsValid();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            mapper.CompileMappings();
        }

        app.UseDefaultFiles();
        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = ctx =>
            {
                ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
            }
        });

        app.UseResponseCaching();

        app.UseETagger();

        app.UseIdentityServer();

        app.UseMiddleware(typeof(ErrorHandlingMiddleware));

        app.UseSwagger();
        app.UseSwaggerUI(ConfigureServicesSwagger.SwaggerSettings);

        // Singleton setup for User Identity
        UserIdentity.Instance.Configure(app.ApplicationServices.GetService<IHttpContextAccessor>()!);            
    }
}
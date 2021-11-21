using Calabonga.Microservice.Module.Web.AppStart.ConfigureServices;
using Calabonga.Microservice.Module.Web.Infrastructure.Auth;
using Calabonga.Microservice.Module.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Calabonga.Microservice.Module.Web.AppStart.Configures
{
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
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseSwagger();
            app.UseSwaggerUI(ConfigureServicesSwagger.SwaggerSettings);

            // Singleton setup for User Identity
            IdentityHelper.Instance.Configure(app.ApplicationServices.GetService<IHttpContextAccessor>()!);
        }
    }
}

using Calabonga.AspNetCore.Micro.Core;
using Calabonga.AspNetCore.Micro.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Calabonga.AspNetCore.Micro.Web.AppStart
{
    /// <summary>
    /// Pipeline configuration
    /// </summary>
    public static class ConfigureApplication
    {
        /// <summary>
        /// Configure pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
                }
            });

            app.UseAuthentication();

            app.UseResponseCaching();

            app.UseETagger();

            app.Map($"{AppData.AuthUrl}", authServer => { authServer.UseIdentityServer(); });

            app.UseSwagger();
            app.UseSwaggerUI(ConfigureServicesSwagger.SwaggerSettings);

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseCors("CorsPolicy");

            // Calabonga: Endpoint Authorization (2019-09-30 08:55 ConfigureApplication)
            // https://aregcode.com/blog/2019/dotnetcore-understanding-aspnet-endpoint-routing/
            // https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-3.0&tabs=visual-studio
            // https://andrewlock.net/comparing-startup-between-the-asp-net-core-3-templates/

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

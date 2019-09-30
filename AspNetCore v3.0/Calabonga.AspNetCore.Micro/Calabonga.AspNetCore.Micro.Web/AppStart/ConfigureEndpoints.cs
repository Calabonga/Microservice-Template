using Microsoft.AspNetCore.Builder;

namespace Calabonga.AspNetCore.Micro.Web.AppStart
{
    /// <summary>
    /// Configure pipeline
    /// </summary>
    public static class ConfigureEndpoints
    {
        public static void Configure(IApplicationBuilder app)
        {
            // Calabonga: Endpoint Authorization (2019-09-30 08:55 ConfigureApplication)
            // https://aregcode.com/blog/2019/dotnetcore-understanding-aspnet-endpoint-routing/
            // https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-3.0&tabs=visual-studio
            // https://andrewlock.net/comparing-startup-between-the-asp-net-core-3-templates/

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

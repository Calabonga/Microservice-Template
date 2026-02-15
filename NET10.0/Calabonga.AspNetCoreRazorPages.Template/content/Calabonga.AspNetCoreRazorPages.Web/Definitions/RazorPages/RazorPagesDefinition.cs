using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.AspNetCoreRazorPages.Domain;

namespace Calabonga.AspNetCoreRazorPages.Web.Definitions.RazorPages;

public class RazorPagesDefinition : AppDefinition
{
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
    }

    public override void ConfigureApplication(WebApplication app)
    {
        app.UseStaticFiles();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseRouting();
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.None,
            Secure = CookieSecurePolicy.Always
        });

        if (app.Environment.IsDevelopment())
        {
            app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
                string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
        }
        app.UseCors(AppData.PolicyName);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages()
            .WithStaticAssets();
    }
}

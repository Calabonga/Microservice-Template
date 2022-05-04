using Calabonga.AuthService.Infrastructure;
using Calabonga.AuthService.Web.Definitions.Base;

namespace Calabonga.AuthService.Web.Definitions.OAuth
{
    public class OAuthDefinition: AppDefinition
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOpenIddict()
                .AddCore(options =>
                {
                    // Configure OpenIddict to use the default entities with a custom key type.
                    options.UseEntityFrameworkCore()
                        .UseDbContext<ApplicationDbContext>()
                        .ReplaceDefaultEntities<Guid>();
                });
        }
    }
}

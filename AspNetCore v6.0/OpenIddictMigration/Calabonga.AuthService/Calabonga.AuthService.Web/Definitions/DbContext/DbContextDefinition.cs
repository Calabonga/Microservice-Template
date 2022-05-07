using Calabonga.AuthService.Infrastructure;
using Calabonga.AuthService.Web.Definitions.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

namespace Calabonga.AuthService.Web.Definitions.DbContext
{
    /// <summary>
    /// ASP.NET Core services registration and configurations
    /// </summary>
    public class DbContextDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(config =>
            {
                // UseInMemoryDatabase - This for demo purposes only!
                // Should uninstall package "Microsoft.EntityFrameworkCore.InMemory" and install what you need. 
                // For example: "Microsoft.EntityFrameworkCore.SqlServer"
                // uncomment line below to use UseSqlServer(). Don't forget setup connection string in appSettings.json 
                config.UseInMemoryDatabase("DEMO-PURPOSES-ONLY");

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need to replace the default OpenIddict entities.
                config.UseOpenIddict<Guid>();
            });


            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
                // configure more options if you need
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddUserStore<ApplicationUserStore>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddUserManager<UserManager<ApplicationUser>>();

            services.AddTransient<ApplicationUserStore>();
        }
    }
}
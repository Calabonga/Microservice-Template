using Calabonga.Microservice.IdentityModule.Infrastructure;
using Calabonga.Microservice.IdentityModule.Web.Definitions.Base;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Calabonga.Microservice.IdentityModule.Web.Definitions.Identity
{
    /// <summary>
    /// Authorization Policy registration
    /// </summary>
    public class AuthorizationMicroserviceDefinition : MicroserviceDefinition
    {
        /// <summary>
        /// Configure services for current microservice
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IdentityOptions>(options =>
            {
            // Password settings.
            options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters = null;
                options.User.RequireUniqueEmail = true;
            });

            services.AddTransient<ApplicationUserStore>();
            services.AddScoped<ApplicationClaimsPrincipalFactory>();

            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddUserStore<ApplicationUserStore>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddRouting(options => options.LowercaseUrls = true);

            var url = configuration.GetSection("IdentityServer").GetValue<string>("Url");

            services.AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(
                    options =>
                    {
                        options.SupportedTokens = SupportedTokens.Jwt;
                        options.Authority = url;
                        options.EnableCaching = true;
                        options.RequireHttpsMetadata = false;
                    });

            services.AddIdentityServer(options =>
            {
                options.Authentication.CookieSlidingExpiration = true;
                options.IssuerUri = url;
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.UserInteraction.LoginUrl = "/Authentication/Login";
                options.UserInteraction.LogoutUrl = "/Authentication/Logout";
            })
                .AddCorsPolicyService<CorsPolicyService>()
                .AddInMemoryPersistedGrants()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddInMemoryApiScopes(IdentityServerConfig.GetAPiScopes())
                .AddAspNetIdentity<ApplicationUser>()
                .AddJwtBearerClientAuthentication()
                .AddProfileService<IdentityProfileService>();


            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, MicroservicePermissionHandler>();

            services.AddAuthorization();
        }

        /// <summary>
        /// Configure application for current microservice
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public override void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseIdentityServer();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            // registering UserIdentity helper as singleton
            UserIdentity.Instance.Configure(app.ApplicationServices.GetService<IHttpContextAccessor>()!);
        }
    }
}
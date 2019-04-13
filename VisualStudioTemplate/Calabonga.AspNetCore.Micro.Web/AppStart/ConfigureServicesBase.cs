using System;
using System.Text;
using AutoMapper;
using Calabonga.AspNetCore.Micro.Core;
using Calabonga.AspNetCore.Micro.Data;
using $safeprojectname$.Infrastructure.Attributes;
using $safeprojectname$.Infrastructure.Services;
using $safeprojectname$.Infrastructure.Settings;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace $safeprojectname$.AppStart
{
    /// <summary>
    /// ASP.NET Core services registration and configurations
    /// </summary>
    public static class ConfigureServicesBase
    {
        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            // file upload dependency
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddUserStore<ApplicationUserStore>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContextPool<ApplicationDbContext>(config =>
            {
                config.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole>();

            services.AddMemoryCache();

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

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

            services.Configure<RouteOptions>(routeOptions =>
            {
                //routeOptions.ConstraintMap.Add("email", typeof(EmailRouteConstraint));
            });

            services.AddOptions();

            services.Configure<CurrentAppSettings>(configuration.GetSection(nameof(CurrentAppSettings)));

            services.AddHttpContextAccessor();
            services.AddResponseCaching();

            var url = configuration.GetSection("IdentityServer").GetValue<string>("Url");
            services.AddIdentityServer(options =>
                {
                    options.Authentication.CookieSlidingExpiration = true;
                    options.IssuerUri = $"{url}{AppData.AuthUrl}";
                })
                .AddInMemoryPersistedGrants()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddAspNetIdentity<ApplicationUser>()
                .AddJwtBearerClientAuthentication()
                .AddProfileService<IdentityProfileService>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.SupportedTokens = SupportedTokens.Jwt;
                    options.Authority = $"{url}{AppData.AuthUrl}";
                    options.EnableCaching = true;
                    options.RequireHttpsMetadata = false;
                });
            services.AddAuthorization();
            services
                .AddMvc(options => { options.Filters.Add<ValidateModelStateAttribute>(); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.DateParseHandling = DateParseHandling.DateTime;
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                });
        }
    }
}
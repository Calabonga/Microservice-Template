using System;
using AutoMapper;
using Calabonga.EntityFrameworkCore.UOW;
using Calabonga.Microservice.Module.Core;
using Calabonga.Microservice.Module.Data;
using Calabonga.Microservice.Module.Web.Infrastructure.Settings;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Microservice.Module.Web.AppStart
{
    /// <summary>
    /// ASP.NET Core services registration and configurations
    /// </summary>
    public static class ConfigureServicesBase
    {
        /// <summary>
        /// ConfigureServices Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            //var currentDirectory = Environment.CurrentDirectory;
            //var directoryName = Path.GetDirectoryName(currentDirectory);
            //var namespaceName = Assembly.GetExecutingAssembly().GetName().Name;
            //var path = Path.Combine(directoryName, $"{namespaceName}.Web");

            

            services.AddDbContext<ApplicationDbContext>(config =>
            {
                config.UseSqlServer(configuration.GetConnectionString(nameof(ApplicationDbContext)));
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            services.AddUnitOfWork<ApplicationDbContext>();

            services.AddMemoryCache();

            services.AddLocalization();

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

            services.AddOptions();

            services.Configure<CurrentAppSettings>(configuration.GetSection(nameof(CurrentAppSettings)));

            services.AddHttpContextAccessor();
            services.AddResponseCaching();

            var url = configuration.GetSection("IdentityServer").GetValue<string>("Url");
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.SupportedTokens = SupportedTokens.Jwt;
                    options.Authority = $"{url}{AppData.AuthUrl}";
                    options.EnableCaching = true;
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization();
            // services
            // .AddMvc(options => { options.Filters.Add<ValidateModelStateAttribute>(); })
            // .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            // .AddJsonOptions(options =>
            // {
            // options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            // options.JsonSerializerOptions.IgnoreNullValues = true;   
            //                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //                    options.SerializerSettings.Formatting = Formatting.Indented;
            //                    options.SerializerSettings.DateParseHandling = DateParseHandling.DateTime;
            // options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            // });


        }
    }
}
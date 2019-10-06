using AutoMapper;
using Calabonga.EntityFrameworkCore.UOW;
using Calabonga.Microservice.Module.Core;
using Calabonga.Microservice.Module.Data;
using Calabonga.Microservice.Module.Web.Infrastructure.Settings;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Microservice.Module.Web.AppStart.ConfigureServices
{
    /// <summary>
    /// ASP.NET Core services registration and configurations
    /// </summary>
    public static class ConfigureServicesCommon
    {
        /// <summary>
        /// ConfigureServices Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<ApplicationDbContext>(config =>
            {
                config.UseSqlServer(configuration.GetConnectionString(nameof(ApplicationDbContext)));
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            services.AddUnitOfWork<ApplicationDbContext>();

            services.AddMemoryCache();

            services.AddRouting();

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            services.AddOptions();
            
            services.Configure<CurrentAppSettings>(configuration.GetSection(nameof(CurrentAppSettings)));

            services.AddLocalization();

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
        }
    }
}
using System;
using AutoMapper;
using Calabonga.AspNetCore.Controllers.Extensions;
using Calabonga.Microservice.IdentityModule.Data;
using Calabonga.Microservice.IdentityModule.Web.Extensions;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Settings;
using Calabonga.Microservice.IdentityModule.Web.Mediator.Behaviors;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Microservice.IdentityModule.Web.AppStart.ConfigureServices
{
    /// <summary>
    /// ASP.NET Core services registration and configurations
    /// </summary>
    public static class ConfigureServicesMediator
    {
        /// <summary>
        /// ConfigureServices Services
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddCommandAndQueries(typeof(Startup).Assembly);
        }
    }
}
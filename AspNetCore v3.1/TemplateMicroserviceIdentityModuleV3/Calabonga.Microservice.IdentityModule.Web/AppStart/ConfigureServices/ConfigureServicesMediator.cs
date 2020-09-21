using System;
using AutoMapper;
using Calabonga.AspNetCore.Controllers.Extensions;
using $ext_projectname$.Data;
using $safeprojectname$.Extensions;
using $safeprojectname$.Infrastructure.Settings;
using $safeprojectname$.Mediator.Behaviors;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$.AppStart.ConfigureServices
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
using Calabonga.AspNetCore.AppDefinitions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Net;
using System.Security.Authentication;

namespace Calabonga.Microservice.Module.Web.Definitions.ErrorHandling;

/// <summary>
/// Custom Error handling
/// </summary>
public class ErrorHandlingDefinition : AppDefinition
{

#if DEBUG
    public override bool Enabled => false;
#else
        public override bool Enabled => true;
#endif

    /// <summary>
    /// Configure application for current application
    /// </summary>
    /// <param name="app"></param>
    public override void ConfigureApplication(WebApplication app) =>
        app.UseExceptionHandler(error => error.Run(async context =>
        {

            context.Response.ContentType = "application/json";
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature is not null)
            {
                // handling all another errors
                Log.Error($"Something went wrong in the {contextFeature.Error}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                if (app.Environment.IsDevelopment())
                {
                    await context.Response.WriteAsync($"INTERNAL SERVER ERROR: {contextFeature.Error}");
                }
                else
                {
                    await context.Response.WriteAsync("INTERNAL SERVER ERROR. PLEASE TRY AGAIN LATER");
                }
            }
        }));

    private static HttpStatusCode GetErrorCode(Exception e)
        => e switch
        {
            ValidationException _ => HttpStatusCode.BadRequest,
            AuthenticationException _ => HttpStatusCode.Forbidden,
            NotImplementedException _ => HttpStatusCode.NotImplemented,
            _ => HttpStatusCode.InternalServerError
        };
}
using System;
using System.Threading.Tasks;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Calabonga.Microservice.IdentityModule.Web.Middlewares
{
    /// <summary>
    /// Custom error handler. It allows to view error messages on UI
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <inheritdoc />
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke middle ware. Entry point
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            try
            {
                var result = JsonConvert.SerializeObject(ExceptionHelper.GetMessages(exception), Formatting.Indented);
                if (result?.Length > 4000)
                {
                    return context.Response.WriteAsync("Error message to long. Please use DEBUG in method HandleExceptionAsync to handle a whole of text of the exception");
                }
                return context.Response.WriteAsync(result);
            }
            catch
            {
                return context.Response.WriteAsync($"{exception.Message} For more information please use DEBUG in method HandleExceptionAsync to handle a whole of text of the exception");
            }
        }
    }
}

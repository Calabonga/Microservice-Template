using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace Calabonga.Microservice.Module.Web.Middlewares
{
    /// <summary>
    /// ETagger extension
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use Custom middleware
        /// </summary>
        /// <param name="app"></param>
        public static void UseETagger(this IApplicationBuilder app)
        {
            app.UseMiddleware<ETagMiddleware>();
        }
    }

    /// <summary>
    /// ETag middleware from Mads Kristensen.
    /// See https://madskristensen.net/blog/send-etag-headers-in-aspnet-core/
    /// </summary>
    public class ETagMiddleware
    {
        private readonly RequestDelegate _next;

        /// <inheritdoc />
        public ETagMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke middleware entry point
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var response = context.Response;
            var originalStream = response.Body;

            using (var ms = new MemoryStream())
            {
                response.Body = ms;

                await _next(context);

                if (IsEtagSupported(response))
                {
                    var checksum = CalculateChecksum(ms);

                    response.Headers[HeaderNames.ETag] = checksum;

                    if (context.Request.Headers.TryGetValue(HeaderNames.IfNoneMatch, out var etag) && checksum == etag)
                    {
                        response.StatusCode = StatusCodes.Status304NotModified;
                        return;
                    }
                }

                ms.Position = 0;
                await ms.CopyToAsync(originalStream);
            }
        }

        private static bool IsEtagSupported(HttpResponse response)
        {
            if (response.StatusCode != StatusCodes.Status200OK)
            {
                return false;
            }

            // The 100kb length limit is not based in science. Feel free to change
            if (response.Body.Length > 100 * 1024)
            {
                return false;
            }

            if (response.Headers.ContainsKey(HeaderNames.ETag))
            {
                return false;
            }

            return true;
        }

        private static string CalculateChecksum(MemoryStream ms)
        {
            string checksum = "";

            using (var algo = SHA1.Create())
            {
                ms.Position = 0;
                byte[] bytes = algo.ComputeHash(ms);
                checksum = $"\"{WebEncoders.Base64UrlEncode(bytes)}\"";
            }

            return checksum;
        }
    }
}
using Calabonga.AspNetCore.AppDefinitions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Diagnostics;
using System.Security.Cryptography;

namespace $safeprojectname$.Definitions.ETagGenerator;

/// <summary>
/// ETag Generator
/// </summary>
public class ETagGeneratorDefinition : AppDefinition
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
        app.Use(async (context, next) =>
        {
            var response = context.Response;
            var originalStream = response.Body;

            await using var ms = new MemoryStream();
            response.Body = ms;

            await next(context);

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
        });


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

        return !response.Headers.ContainsKey(HeaderNames.ETag);
    }

    private static string CalculateChecksum(MemoryStream ms)
    {
        using var algorithm = SHA1.Create();
        ms.Position = 0;
        var bytes = algorithm.ComputeHash(ms);
        return $"\"{WebEncoders.Base64UrlEncode(bytes)}\"";
    }
}
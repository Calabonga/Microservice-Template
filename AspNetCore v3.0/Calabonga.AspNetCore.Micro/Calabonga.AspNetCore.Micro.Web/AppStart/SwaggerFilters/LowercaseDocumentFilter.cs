using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Calabonga.AspNetCore.Micro.Web.AppStart.SwaggerFilters
{
    /// <summary>
    /// Lowercase routes filter
    /// </summary>
    public class LowercaseDocumentFilter : IDocumentFilter
    {
        /// <inheritdoc />
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            //            swaggerDoc.Paths = swaggerDoc.Paths.ToDictionary(entry => LowercaseEverythingButParameters(entry.Key), entry => entry.Value);
        }

        private static string LowercaseEverythingButParameters(string key)
        {
            return string.Join('/', key.Split('/').Select(x => x.Contains("{") ? x : x.ToLower()));
        }
    }
}

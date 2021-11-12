using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Microservice.IdentityModule.Web.Extensions;

/// <summary>
/// Extensions for application class
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Use special route slugify for Pegasus routing
    /// </summary>
    /// <param name="options"></param>
    public static void UseRouteSlugify(this MvcOptions options) => options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
}

/// <summary>
/// Special route naming convention
/// </summary>
public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    /// <inheritdoc />
    public string TransformOutbound(object value) => value == null 
        ? null
        : Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
}
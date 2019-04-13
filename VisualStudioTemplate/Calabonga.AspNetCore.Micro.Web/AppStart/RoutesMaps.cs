using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace $safeprojectname$.AppStart
{
    /// <summary>
    /// Routes mapper
    /// </summary>
    public class RoutesMaps
    {
        /// <summary>
        /// Map Routes
        /// </summary>
        /// <param name="routes"></param>
        public static void MapRoutes(IRouteBuilder routes)
        {
            routes.MapRoute(
                name: "Token",
                template: "auth/connect/token");
            
            routes.MapRoute(
                name: "Auth",
                template: "auth/connect/authorize");

            routes.MapRoute(
                name: "DefaultApi",
                template: "api/{action}",
                defaults: new { controller = "api" });
        }
    }
}

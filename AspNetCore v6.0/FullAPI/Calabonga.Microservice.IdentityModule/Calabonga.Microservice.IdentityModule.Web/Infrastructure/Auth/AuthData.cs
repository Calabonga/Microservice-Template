using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Auth
{
    public static class AuthData
    {
        public const string AuthSchemes = CookieAuthenticationDefaults.AuthenticationScheme + "," + JwtBearerDefaults.AuthenticationScheme;
    }
}
using Microsoft.AspNetCore.Authentication.Cookies;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace Calabonga.AuthService.Web.Definitions.OpenIddict
{
    public static class AuthData
    {
        public const string AuthSchemes = CookieAuthenticationDefaults.AuthenticationScheme + "," + OpenIddictServerAspNetCoreDefaults.AuthenticationScheme;
    }
}
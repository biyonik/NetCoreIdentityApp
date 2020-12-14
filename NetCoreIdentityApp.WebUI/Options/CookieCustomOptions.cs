using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace NetCoreIdentityApp.WebUI.Options
{
    public static class CookieCustomOptions
    {
        private static CookieBuilder _cookieBuilder;

        public static Action<CookieAuthenticationOptions> GetCookieAuthOptions()
        {
            _cookieBuilder = new CookieBuilder()
            {
                Name = "NetCoreIdentityAppCookie",
                HttpOnly = true,
                Expiration = TimeSpan.FromDays(60),
                SameSite = SameSiteMode.Lax,
                SecurePolicy = CookieSecurePolicy.SameAsRequest
            };

            return options =>
            {
                options.Cookie = _cookieBuilder;
                options.LoginPath = new PathString("/Home/SignIn");
                options.SlidingExpiration = true;
            };
        }
    }
}
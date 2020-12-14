using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace NetCoreIdentityApp.WebUI.Options
{
    public class CookieCustomOptions
    {
        private CookieBuilder _cookieBuilder;

        public CookieCustomOptions()
        {
            _cookieBuilder = new CookieBuilder();
        }

        public Action<CookieAuthenticationOptions> GetCookieAuthOptions()
        {
            _cookieBuilder.Name = "NetCoreIdentityAppCookie";
            _cookieBuilder.HttpOnly = true;
            _cookieBuilder.SameSite = SameSiteMode.Lax;
            _cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            

            return options =>
            {
                options.Cookie = _cookieBuilder;
                options.LoginPath = new PathString("/Home/SignIn");
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(60);
            };
        }
    }
}
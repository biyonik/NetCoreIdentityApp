using System;
using Microsoft.AspNetCore.Identity;

namespace NetCoreIdentityApp.WebUI.Options
{
    public static class IdentityCustomOptions
    {
        public static Action<IdentityOptions> Options()
        {
            return options =>
            {
                options.Password = SetPasswordOptions(); 
            };
        }

        private static PasswordOptions SetPasswordOptions()
        {
            return new PasswordOptions()
            {
                RequiredLength = 6,
                RequireNonAlphanumeric = false,
                RequireLowercase = false,
                RequireUppercase = false,
                RequireDigit = false
            };
        }
    }
}
using System;
using Microsoft.AspNetCore.Identity;

namespace NetCoreIdentityApp.WebUI.Options
{
    public class IdentityCustomOptions
    {
        public static Action<IdentityOptions> Options()
        {
            return options =>
            {
                options.Password = SetPasswordOptions();
                options.User = SetUserOptions();
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

        private static UserOptions SetUserOptions()
        {
            return new UserOptions()
            {
                RequireUniqueEmail = true
            };
        }
    }
}
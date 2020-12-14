using Microsoft.AspNetCore.Identity;

namespace NetCoreIdentityApp.WebUI.Describers
{
    public class CustomIdentityErrorDescriber: IdentityErrorDescriber
    {
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "InvalidUserName",
                Description = $"Bu kullanıcı adı ({userName}) geçersizdir!"
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = "DuplicateEmail",
                Description = $"Bu e-posta adresi ({email}) kullanılmaktadır!"
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = $"Parolanız çok kısa! Lütfen {length} karakterden daha uzun bir parola giriniz."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = $"Bu kullanıcı adı ({userName}) zaten kullanılmakta!"
            };
        }
    }
}
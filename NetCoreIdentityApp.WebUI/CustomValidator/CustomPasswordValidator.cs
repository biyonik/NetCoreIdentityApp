using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NetCoreIdentityApp.WebUI.Models;

namespace NetCoreIdentityApp.WebUI.CustomValidator
{
    public class CustomPasswordValidator: IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> identityErrorList = new List<IdentityError>();
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                if (!user.Email.Contains(user.UserName))
                {
                    IdentityError passwordContainUserName = new IdentityError()
                    {
                        Code = "ContainUserName",
                        Description = "Parola, kullanıcı adı içeremez!"
                    };
                    identityErrorList.Add(passwordContainUserName);
                }
            }

            if (password.ToLower().Contains("1234"))
            {
                IdentityError passwordContainConsecutiveNumber = new IdentityError()
                {
                    Code = "ContainConsecutiveNumber",
                    Description = "Parola, ardışık sayı içeremez!"
                };
                identityErrorList.Add(passwordContainConsecutiveNumber);
            }

            if (password.ToLower().Contains(user.Email.ToLower()))
            {
                IdentityError passwordContainEmailAddress = new IdentityError()
                {
                    Code = "ContainEMailAddress",
                    Description = "Parola, e-posta adresini içeremez!"
                };
                identityErrorList.Add(passwordContainEmailAddress);
            }

            return Task.FromResult(identityErrorList.Count == 0
                            ? IdentityResult.Success 
                            : IdentityResult.Failed(identityErrorList.ToArray()));
        }
    }
}
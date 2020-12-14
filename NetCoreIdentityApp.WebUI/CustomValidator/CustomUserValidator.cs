using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NetCoreIdentityApp.WebUI.Models;

namespace NetCoreIdentityApp.WebUI.CustomValidator
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            string[] digits = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            List<IdentityError> identityErrors = new List<IdentityError>();
            IdentityError identityError = new IdentityError();
            
            foreach (string digit in digits)
            {
                if (user.UserName[0].ToString() == digit)
                {
                    identityError.Code = "UserNameContainFirstLetterDigit";
                    identityError.Description = "Kullanıcı adı sayısal bir karakter ile başlayamaz!";
                    identityErrors.Add(identityError);
                }
            }

            return Task.FromResult(identityErrors.Count == 0
                            ? IdentityResult.Success 
                            : IdentityResult.Failed(identityErrors.ToArray()));
        }
    }
}
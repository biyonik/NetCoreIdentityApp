using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreIdentityApp.WebUI.Helpers;
using NetCoreIdentityApp.WebUI.Models;
using NetCoreIdentityApp.WebUI.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;


namespace NetCoreIdentityApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterViewModel userRegisterViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName =  userRegisterViewModel.UserName,
                    Email =  userRegisterViewModel.Email,
                    PhoneNumber = userRegisterViewModel.PhoneNumber
                };
                IdentityResult identityResult = await _userManager.CreateAsync(appUser,userRegisterViewModel.Password);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("SignIn", "Home");
                }
                else if (identityResult.Errors.Count() > 0)
                {
                    foreach (IdentityError identityResultError in identityResult.Errors)
                    {
                        ModelState.AddModelError("", identityResultError.Description);
                    }
                }
            }
            return View(userRegisterViewModel);
        }
        
        public IActionResult SignIn(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByNameAsync(userLoginViewModel.UserName);
                if (appUser != null)
                {
                    // Kullanıcı kitli mi?
                    if (await _userManager.IsLockedOutAsync(appUser))
                    {
                        ModelState.AddModelError("","Hesabınız bir süreliğine kilitlenmiştir! Lütfen daha sonra tekrar deneyiniz.");
                    }
                    await _signInManager.SignOutAsync();
                    SignInResult signInResult =  await _signInManager.PasswordSignInAsync(appUser, userLoginViewModel.Password, userLoginViewModel.RememberMe, false);
                    if (signInResult.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(appUser);
                        if (TempData["ReturnUrl"] != null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }
                        return RedirectToAction("Index", "Member");
                    }
                    else
                    {
                        await _userManager.AccessFailedAsync(appUser);
                        int fail = await _userManager.GetAccessFailedCountAsync(appUser);
                        ModelState.AddModelError("", $"{fail} kez başarısız oldunuz. {3-fail} deneme hakkınız kaldı.");
                        if (fail == 3)
                        {
                            await _userManager.SetLockoutEndDateAsync(appUser,
                                new DateTimeOffset(DateTime.Now.AddMinutes(20)));
                            ModelState.AddModelError("","Hesabınız 3 başarısız giriş denemesinden dolayı 20 dakika süre ile kitlenmiştir!");
                        } else
                        {
                            ModelState.AddModelError("", $"Geçeriz kullanıcı adı veya şifre!");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(userLoginViewModel.UserName), $"{userLoginViewModel.UserName} isimli kullanıcı sistemde bulunamadı!");
                }
            } 
            return View(userLoginViewModel);
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(UserResetPasswordViewModel userResetPasswordViewModel)
        {
            AppUser appUser = _userManager.FindByEmailAsync(userResetPasswordViewModel.Email).Result;
            if (appUser != null)
            {
                string passwordResetToken = _userManager.GeneratePasswordResetTokenAsync(appUser).Result;
                string passwordResetLink = Url.Action("ResetPasswordConfirm", "Home", new
                {
                    userId = appUser.Id,
                    token = passwordResetToken
                }, HttpContext.Request.Scheme);
                
                PasswordReset.PasswordResetSendEmail(passwordResetLink);
                ViewBag.status = "successfull";
                return View();
            }
            else
            {
                ModelState.AddModelError(nameof(userResetPasswordViewModel.Email), "Bu e-posta adresine bağlı kullanıcı bulunamadı!");
                return View(userResetPasswordViewModel);
            }
        }

        public void ResetPasswordConfirm()
        {
            
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                    await _signInManager.SignOutAsync();
                    SignInResult signInResult =  await _signInManager.PasswordSignInAsync(appUser, userLoginViewModel.Password, userLoginViewModel.RememberMe, false);
                    if (signInResult.Succeeded)
                    {
                        if (TempData["ReturnUrl"] != null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }
                        return RedirectToAction("Index", "Member");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(userLoginViewModel.UserName), $"{userLoginViewModel.UserName} isimli kullanıcı sistemde bulunamadı!");
                }
            } 
            return View(userLoginViewModel);
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreIdentityApp.WebUI.Models;
using NetCoreIdentityApp.WebUI.ViewModels;

namespace NetCoreIdentityApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(UserLoginViewModel userLoginViewModel)
        {
            return View();
        }
    }
}

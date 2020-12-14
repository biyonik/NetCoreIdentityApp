using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreIdentityApp.WebUI.Models;
using System.Linq;

namespace NetCoreIdentityApp.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            IQueryable<AppUser> users = _userManager.Users;
            return View(users);
        }
    }
}

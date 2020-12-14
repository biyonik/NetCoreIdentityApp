using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreIdentityApp.WebUI.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
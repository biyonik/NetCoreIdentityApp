using Microsoft.AspNetCore.Mvc;

namespace NetCoreIdentityApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

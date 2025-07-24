using Microsoft.AspNetCore.Mvc;

namespace AmberAlerting.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
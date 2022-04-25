using Microsoft.AspNetCore.Mvc;

namespace DangerSwap.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

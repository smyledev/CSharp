using Microsoft.AspNetCore.Mvc;

namespace CryptLab1WebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Models;

namespace Secret_Santa_MVC.Controllers
{
    public class SantaController : Controller
    {
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult Start ()
        {
            return View();
        }
        public IActionResult Register(User user)
        {

            return View();
        }
    }
}

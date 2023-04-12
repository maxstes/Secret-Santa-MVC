using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Models;

namespace Secret_Santa_MVC.Controllers
{
    public class SantaController : Controller
    {
        public IActionResult Start ()
        {
            return View();
        }
        public IActionResult RegisterForm(Room form)
        {

            return View();
        }
    }
}

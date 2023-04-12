using Microsoft.AspNetCore.Mvc;

namespace Secret_Santa_MVC.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

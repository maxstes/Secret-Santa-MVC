using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Email;
using Secret_Santa_MVC.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Secret_Santa_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Test()
        {
            using FileStream openStream = System.IO.File.OpenRead("secrets.json");
            OAuthClass? OAuth =
                 JsonSerializer.Deserialize<OAuthClass>(openStream);
            return View(OAuth);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      //  public IActionResult Error()
        //{
          //  return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
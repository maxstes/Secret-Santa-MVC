using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Models;
using Secret_Santa_MVC.DBClass;

namespace Secret_Santa_MVC.Controllers
{
    public class SantaController : Controller
    {
        readonly SantaDatabase _database;
        readonly Commands _commands;
        public SantaController() 
        {
            _database = new SantaDatabase();
            _commands = new Commands();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if(user == null) { Problem("From emply"); }
            user.Role = "user";
            user.DateRegister = DateTime.Now;

            if (_commands.CheckEmail(user) ==  true)
            {
//TODO Переадрисация на страницу с собщением о занятой почте
                return BadRequest("Email busy");
            }
            _database.Users.Add(user);
            await _database.SaveChangesAsync();
            return RedirectToAction("Index","Santa");
        }
    }
}

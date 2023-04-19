using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Models;
using System.Security.Claims;

namespace Secret_Santa_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly SantaDatabase _database;
        public LoginController()
        {
            _database= new SantaDatabase();
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginForm user)
        {
            var context = ControllerContext.HttpContext;

            if (user.Email == null && user.Password == null) { return NotFound("LoginController stroka17"); }

            User? thisUser = _database.Users.FirstOrDefault(p => p.Email == user.Email && p.Email == user.Email);
            if (user is null) return NotFound("thisUser is null");
            var claims = new List<Claim>
            { new Claim(ClaimsIdentity.DefaultNameClaimType,thisUser.Email),
              new Claim(ClaimsIdentity.DefaultRoleClaimType,thisUser.Role),
            };
            var claimIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
            await context.SignInAsync(claimsPrincipal);
            return RedirectToAction("AdminPanel"); 
        }
        [HttpGet]
        public string TestAction()
        {
            var context = ControllerContext.HttpContext;
#warning koment
            //if (context.User) ;
            return "";
        }

        [HttpGet]
        [Authorize(Roles ="admin")]
        public string AdminPanel()
        {
            return "Admin Panel";
        }
        [HttpGet]
        [Authorize(Roles ="admin, user")]
        public string ForAll()
        {
            var context = ControllerContext.HttpContext;
            var login = context.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
            var role = context.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType);

            return $"Name: {login?.Value} \nRole: {role?.Value}";
        }
        [HttpGet]
        public async Task<string> Out()
        {
            var context = ControllerContext.HttpContext;
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return " Data delete";
        }
    }
}

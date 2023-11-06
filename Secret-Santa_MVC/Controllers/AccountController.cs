using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Secret_Santa_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
    public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                RedirectToAction("Test", "Account");   
            }
            return View();
        }
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View(); 
        }
        [HttpGet("login")]
        public IActionResult Authenticate()
        {
            return View();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if(!ModelState.IsValid) return BadRequest(request);

            var user = new ApplicationUser
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.Email,
                DateRegister = DateTime.UtcNow,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded) 
            {
                await _userManager.AddToRoleAsync(user, RoleConsts.Member);
                await _signInManager.SignInAsync(user, false);
                Console.WriteLine("Register success");
                return RedirectToAction("Index", "Account");
            }
            return BadRequest(result.Errors);
        }
        
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authenticate(AuthRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(request); }
            else {
                var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);
                if (result.Succeeded)
                {
                    Console.WriteLine("Login cuccess");
                    return RedirectToAction("Test", "Account");
                }
                else return BadRequest("Not correct login or (and) password");
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }
        
        [HttpGet]
        public IActionResult Test()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public IActionResult Home()
        {
            return View();
        }
        
    }
}

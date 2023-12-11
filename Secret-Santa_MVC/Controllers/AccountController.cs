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
        private readonly ILogger<AccountController> _logger;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
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
                _logger.LogInformation($"{request.Email} Register");
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
                    _logger.LogInformation($"Authenticate {request.Email}");
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
            _logger.LogInformation($"{User?.Identity?.Name} Log Out");
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

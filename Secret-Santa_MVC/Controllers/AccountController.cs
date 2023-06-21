using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Secret_Santa_MVC.Extensions;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using System;
using Secret_Santa_MVC.Services.Interface;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Secret_Santa_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SantaContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IAppUserService _appUserService;

        public AccountController(SignInManager<ApplicationUser> signInManager,IAppUserService appUserService, SantaContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            _appUserService = appUserService;
        }
        [HttpGet]
    public IActionResult Index()
        {
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
                await _signInManager.SignInAsync(user, false);
                Console.WriteLine("Register cuccess");
                return RedirectToAction("Index", "Account");
            }
            return View(request);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(AuthRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(request); }
            else {
                var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
                if (result.Succeeded)
                {
                    // TODO добавити перевірку URL https://metanit.com/sharp/aspnet5/16.4.php
                    // if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)
                    Console.WriteLine("Login cuccess");
                    return RedirectToAction("Test", "Account");
                }
                else return BadRequest("Not correct login or (and) password");
            }
        }
        [HttpGet]
        public IActionResult Test()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Service;
using Secret_Santa_MVC.Tools;

namespace Secret_Santa_MVC.Controllers
{
    public class AccountSettingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly  EmailService _emailService;
        private readonly UserTools _userTools;
        public AccountSettingsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _userTools = new UserTools();
            _emailService = new EmailService();
        }
        [HttpGet("/Setting")]
        public IActionResult Index()
        {
            //TODO Create to VIEWS
            return View();
        }
        [HttpGet("/EmailConfirmed")]
        public async Task<IActionResult> EmailConfirmed(ApplicationUser? user)
        {
            user = _userTools.GetUser(User.Identity.Name);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "AccountSettings",
                new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme);
            await _emailService.SendEmailAsync(user.Email,
                "Confirm your account",
                $"Confirm your email by clicking on the link" +
                $": <a href='{callbackUrl}'>link</a>");
            return Content("To confirm email check your" +
                "email and follow the link in the email");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId,string code)
        {
            if(userId == null || code == null)
            {
                return View("Error");
            }
            var user = _userManager.FindByIdAsync(userId).Result;
            if(user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Account");
            else
                return View("Error");
        }
    }
}

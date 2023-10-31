using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Models.Identity;
using Secret_Santa_MVC.Service;
using Secret_Santa_MVC.Tools;

namespace Secret_Santa_MVC.Controllers
{
    [Authorize]
    public class AccountSettingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailService _emailService;
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
            return View();
        }
        [HttpGet("/EmailConfirmed")]
        public async Task<IActionResult> EmailConfirmed(ApplicationUser? user)
        {
            user = await _userTools.GetUser(User.Identity.Name);
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
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Account");
            else
                return View("Error");
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }

            ApplicationUser user = await _userManager.FindByIdAsync(Convert.ToString(model.Id));
            if (user != null)
            {
                IdentityResult result =
                    await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return NotFound("There is no such user");
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action(
                "ResetPassword",
                "AccountSettings",
                new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme);

            await _emailService.SendEmailAsync(model.Email,
                "Confirm reset password",
                $"Confirm reset your password by clicking on the link" +
                $": <a href='{callbackUrl}'>link</a>");

            return Content("Open the letter you received in the mail and follow the link");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? BadRequest("Error") : View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model); 
            
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user == null)
            {
                return BadRequest("Page not found:(");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
                return Content("Operation successful");
            // 
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
                return BadRequest("Operation not successful ");
        }
    }
}

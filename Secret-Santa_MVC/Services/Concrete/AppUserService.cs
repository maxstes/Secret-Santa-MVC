using Microsoft.AspNetCore.Identity;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Models.Identity;
using Secret_Santa_MVC.Services.Interface;

namespace Secret_Santa_MVC.Services.Concrete
{
    public class AppUserService:IAppUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AppUserService(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<SignInResult> Login (AuthRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Email) ;
            SignInResult result;
            if (user != null)
            {
                result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                return result;
            }
            result = SignInResult.Failed;
            return result;
        }
    }
}

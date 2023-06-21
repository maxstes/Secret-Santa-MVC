using Microsoft.AspNetCore.Identity;
using Secret_Santa_MVC.Models.Identity;

namespace Secret_Santa_MVC.Services.Interface
{
    public interface IAppUserService
    {
        Task<SignInResult> Login(AuthRequest model);
    }
}

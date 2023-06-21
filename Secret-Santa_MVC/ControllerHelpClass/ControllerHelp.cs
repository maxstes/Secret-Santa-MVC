using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Services.Interface;

namespace Secret_Santa_MVC.ControllerHelpClass
{
    public class ControllerHelp
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SantaContext _context;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        public ControllerHelp(ITokenService tokenService, SantaContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager= userManager;
            _context = context;
            _tokenService= tokenService;
            _configuration= configuration;

        }

    }
}

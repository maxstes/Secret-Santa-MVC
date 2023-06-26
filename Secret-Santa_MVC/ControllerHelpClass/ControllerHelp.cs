using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;

namespace Secret_Santa_MVC.ControllerHelpClass
{
    public class ControllerHelp
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SantaContext _context;
        private readonly IConfiguration _configuration;
        public ControllerHelp( SantaContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager= userManager;
            _context = context;
            _configuration= configuration;

        }

    }
}

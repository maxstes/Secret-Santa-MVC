using Microsoft.EntityFrameworkCore;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;

namespace Secret_Santa_MVC.Tools
{
    public class UserTools
    {
        private readonly SantaContext _context = new SantaContext();
        public async Task<ApplicationUser> GetUser(string Name)
        {
            return _context.Users
                .Where(x => x.UserName == Name)
                .FirstOrDefaultAsync()
                .Result;
        }
        public async Task<ApplicationUser> GetUser(long id)
        {
            return _context.Users
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync()
                .Result;
        }
    }
}

using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;

namespace Secret_Santa_MVC.Tools
{
    public class UserTools
    {
        private readonly SantaContext _context;
        public UserTools()
        {
            _context = new SantaContext();
        }
        public ApplicationUser GetUser(string Name)
        {
            return _context.Users
                .Where(x => x.UserName == Name)
                .FirstOrDefault();

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Models;

namespace Secret_Santa_MVC.DBClass
{
    public class Commands
    {
        readonly SantaContext _database;
        public Commands()
        {
            _database = new SantaContext();
        }
        public bool CheckEmail(ApplicationUser user)
        {
            if (user == null)
            {
                throw new Exception("User == null,Commands ,16");
            }
            bool result = _database.Users.Any(p => p.Email == user.Email);
            return result;
        }
    }
}

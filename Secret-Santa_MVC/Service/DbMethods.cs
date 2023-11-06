using Microsoft.EntityFrameworkCore;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;

namespace Secret_Santa_MVC.Service
{
    public class DbMethods
    {
        readonly SantaContext _context = new SantaContext();

        public async Task RecordInDb(WhoWhoh who)
        {
            if (who == null)
            {
                throw new Exception("Data for Record in DB emply");
            }
            await _context.WhoWhoh
                 .AddAsync(who);

            _context.SaveChanges();
        }
        
    }
}

using Microsoft.EntityFrameworkCore;

namespace Secret_Santa_MVC.Models
{
    public class SantaDatabase :DbContext
    {
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}

using Microsoft.EntityFrameworkCore;

namespace Secret_Santa_MVC.Models
{
    public class SantaDatabase :DbContext
    {
        public DbSet<RoomForm> RegForm { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public SantaDatabase()
        {
          //  Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-HIR5786\SQLEXPRESS;Database=Santa;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}

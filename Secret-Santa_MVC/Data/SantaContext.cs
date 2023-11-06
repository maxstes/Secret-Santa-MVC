using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;

namespace Secret_Santa_MVC.Data
{
    public class SantaContext : IdentityDbContext<ApplicationUser, IdentityRole<long>,long>
    {
        public SantaContext()
        {
          // Database.EnsureCreated();
        }
        public SantaContext(DbContextOptions<SantaContext> options):base(options)
        {
           //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //User Id = SA;Password=Zabiyaka1337;
            optionsBuilder.UseSqlServer(
@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Santa-Identety;Integrated Security=True;Connect Timeout=30;
Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        public DbSet<RoomCreated> RoomCreated { get; set; } = null!;
        public DbSet<RoomGuest> RoomGuests { get; set; } = null!;
        public DbSet<WhoWhoh> WhoWhoh { get; set; } = null!;
    }
}

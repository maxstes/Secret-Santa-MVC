using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;

namespace Secret_Santa_MVC.Data
{
    public sealed class SantaContext : IdentityDbContext<ApplicationUser, IdentityRole<long>,long>
    {
        public SantaContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-HIR5786\SQLEXPRESS;Database=Santa-Identety;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        //public SantaContext (DbContextOptions<SantaContext> options) : base (options)
        //{
        // Database.Migrate();
        //}

        public DbSet<Application> Applications { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
       // public DbSet<SampleData> SampleDate { get; set; } = null!;
    }
}

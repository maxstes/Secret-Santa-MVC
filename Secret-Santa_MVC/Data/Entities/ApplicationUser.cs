using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Data.Entities
{
    public class ApplicationUser :IdentityUser<long>
    {
        public string? FullName { get; set; }
        public DateTime DateRegister { get; set; }
        //TODO Прибрати це лайно що залишилося з токенів
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

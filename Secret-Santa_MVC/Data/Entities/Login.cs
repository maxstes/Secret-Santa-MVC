using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Data.Entities
{
    public class Login
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}

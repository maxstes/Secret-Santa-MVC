using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Models.Identity
{
    public class AuthRequest
    {
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Models.Identity
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

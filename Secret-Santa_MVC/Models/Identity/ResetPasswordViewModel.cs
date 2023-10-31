using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Models.Identity
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password must contain at least" +
            "6 charates", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage = "Password mishmatch")]
        public string ConfirmPassword { get; set;}

        public string Code { get; set; }
    }
}

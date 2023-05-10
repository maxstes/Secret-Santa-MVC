using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Models.Identity
{
    public class RegisterRequest
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password",ErrorMessage ="Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set;} = null!;

        [Required]
        [Display(Name="FullName")]
        public string FullName { get; set; } = null!;

    }
}

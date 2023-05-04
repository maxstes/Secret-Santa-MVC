using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Models
{
    public enum FixationType
    {
        [Display(Name = "Fixed(10% formalin)")]
        Fixed10Formalin,

        [Display(Name = "Fresh")]
        Fresh
    }
}

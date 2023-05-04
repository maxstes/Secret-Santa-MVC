using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Models
{
    public enum Gender
    {
        [Display(Name ="Man")]
        Man,

        [Display(Name="Woman")]
        Woman
    }
}

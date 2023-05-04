using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Models
{
    public enum SampleType
    {
        //
        [Display(Name = "Опухоль(Целиком)")]
        TumorWhole,

        //
        [Display(Name ="Опухоль фрагмент")]
        TumorFragment
    }
}

using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Models
{
    public enum ApplicationStatus
    {
        // Created
        [Display(Name ="Created")]
        Created,

        //InProgress
        [Display(Name ="InProgress")]
        InProgress,

        //Finished
        [Display(Name ="Finished")]
        Finished
    }
}

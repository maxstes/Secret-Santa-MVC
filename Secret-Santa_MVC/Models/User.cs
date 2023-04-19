using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3,ErrorMessage ="Enter more 3 symbols")]
        [MaxLength(20,ErrorMessage = "Enter less 20 symbols")]
        public string? FullName { get; set; }
        public string? Wish { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(3,ErrorMessage ="Enter more 3 symbols")]
        [MaxLength(20,ErrorMessage ="Enter less 20 symbols")]
        public string? Password { get; set; }
        [Required]
        public int Age { get; set; }
        
        public DateTime DateRegister { get; set; }

        public string? Role { get; set; }
        //[Compare("Password")]
        //public string? Password1 { get; set; }

        //public int RoomId { get; set; }

    }
}

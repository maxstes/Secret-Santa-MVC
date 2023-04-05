using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Wish { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [PasswordPropertyText]
        public string? Password { get; set; }
        //[Compare("Password")]
        //public string? Password1 { get; set; }

        //public int RoomId { get; set; }

    }
}

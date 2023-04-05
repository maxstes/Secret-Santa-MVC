using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Secret_Santa_MVC.Models
{
    public class RoomForm
    {
        [Key]
        public int IdRoom { get; set; }
        public string? NameRoom { get; set; }
        public string? Description { get; set; }
        public string? LinkRoom { get; set; }
        public float Budget { get; set; }

        
        public int UserId { get; set; }
        public List<User> Users { get; set; } = new();
    }
}

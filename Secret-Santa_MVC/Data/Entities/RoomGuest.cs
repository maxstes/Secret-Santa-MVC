using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Data.Entities
{
    public class RoomGuest
    {
        [Key]
        public int IdGuest { get; set; }
        public int IdUser { get; set; }
        public string  Wish { get; set; }
        public string? NameGuest { get; set; }

        public List<RoomCreated> RoomCreateds { get; set; } = new();
    }
}

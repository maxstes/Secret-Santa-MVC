

using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Data.Entities
{
    public class RoomGuest
    {
        [Key]
        public int IdGuest { get; set; }
        public RoomCreated? IdRoom { get; set; }
        public int IdUser { get; set; }
        public string?  Wish { get; set; }
        public string NameGuest { get; set; }
    }
}

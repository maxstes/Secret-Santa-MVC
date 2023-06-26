using System.ComponentModel.DataAnnotations;

namespace Secret_Santa_MVC.Data.Entities
{
    public class RoomCreated
    {
        [Key]
        public int IdRoom { get; set; }
        public int OwnerRoomId { get; set; } 
        public string NameRoom { get; set; }
        public string? InviteLink { get; set; }
        public float MaxPriceGift { get; set; }
        public float MinPriceGift { get;set; }
    }
}

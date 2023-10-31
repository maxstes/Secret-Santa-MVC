namespace Secret_Santa_MVC.Data.Entities
{
    public class GetRooms
    {
        public int IdRoom { get; set; }
        public string NameRoom { get; set; }
        public int CountUser { get; set; }
        public string? OwnerName { get; set; }
    }
}

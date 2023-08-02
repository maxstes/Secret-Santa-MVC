namespace Secret_Santa_MVC.Data.Entities
{
    public class SpecificRoom
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public bool Status { get; set; }
        public int CountPlayers { get; set; }

    }
}

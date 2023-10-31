namespace Secret_Santa_MVC.Data.Entities
{
    public class WhoWhoh
    {
        public int Id { get; set; }
        public int IdRoom { get; set; }
        public int UserIdWho { get; set; }
        public int UserIdWhoh { get; set; }
        public string Wish { get; set; }

    }
}

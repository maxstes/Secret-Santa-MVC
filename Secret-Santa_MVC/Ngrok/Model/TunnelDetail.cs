namespace Secret_Santa_MVC.Ngrok.Model
{
    public class TunnelDetail
    {
        public string? name { get; set; }
        public string? ID { get; set; }
        public string? uri { get; set; }
        public string? public_url { get; set; }
        public string? proto { get; set; }
        public ConfigDetail? config { get; set; }
    }
}

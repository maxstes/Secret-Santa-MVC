using Newtonsoft.Json;

namespace Secret_Santa_MVC.Service.GoogleOuth
{
    public class TokenResult
    {
        [JsonProperty("acces_token")]
        public string AccesToken { get; set; }
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}

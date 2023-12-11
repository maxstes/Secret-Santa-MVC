using Newtonsoft.Json;
using NgrokApi;
using Secret_Santa_MVC.Ngrok.Model;
using Secret_Santa_MVC.TelegramLog.Data;

namespace Secret_Santa_MVC.Ngrok
{
    public class NgrokPublicUrlReader
    {
        static HttpClient client = new HttpClient();
        private static ILogger<NgrokPublicUrlReader>? _logger;
        private string? PublicUrl;
        private readonly string ApiUrl = AppSettings.ApiUrl;
        public NgrokPublicUrlReader(ILogger<NgrokPublicUrlReader> logger) 
        {
            _logger = logger;
        }
        public async Task<string> GetPublicUrl()
        {
            var Response = GetResponse(ApiUrl);
            var Url = GetUrl(Response.Result);
            return Url.Result;
        }
        public async Task<HttpResponseMessage> GetResponse(string ApiUrl)
        { 
            try
            {
                HttpResponseMessage? response = await client.GetAsync(ApiUrl);
                return response;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<string> GetUrl(HttpResponseMessage response)
        { 

            string responseBody = await response.Content.ReadAsStringAsync();

            NgrokTunnels? ngrokTunnel = JsonConvert.DeserializeObject<NgrokTunnels>(responseBody);

            foreach (var tunnel in ngrokTunnel.Tunnels)
            {
                PublicUrl = tunnel.public_url;
            }
            return PublicUrl;
        }
        public async Task InstallPublicUrl()
        {
            string value = await GetPublicUrl();
             
            AppSettings.Url = value;
        }
    }
}

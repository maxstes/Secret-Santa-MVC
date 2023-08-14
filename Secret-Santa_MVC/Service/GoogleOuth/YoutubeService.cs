using Secret_Santa_MVC.Helpers;

namespace Secret_Santa_MVC.Service.GoogleOuth
{
    public static class YoutubeService
    {
        private const string YoutubeApiEndPoint = "https://www.googleapis.com/youtube/v3/channels";
        public static async Task<string> GetMyChannelId(string accessToken)
        {
            var queryParams = new Dictionary<string, string>()
            {
                {"mine","true" }
            };
            var response = await HttpClientHelper.SendGetRequest<dynamic>(YoutubeApiEndPoint,queryParams,accessToken);

            string channelId = response.items[0].id;
            return channelId;
        }
        public static async Task UpdateChanelDescription(string accessToken,string channelId,
            string newDescription)
        {
            var queryParams = new Dictionary<string, string>
            {
                {"part", "brandingSetting" }
            };

            var body = new
            {
                id = channelId,
                brandingSettings = new
                {
                    channel = new
                    {
                        description = newDescription
                    }
                }
            };
             
            await HttpClientHelper.
                SendPutRequest(YoutubeApiEndPoint,queryParams,body,accessToken);
        }
    }
}

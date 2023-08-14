using Secret_Santa_MVC.Email;
using System.Text.Json;

namespace Secret_Santa_MVC.Service
{
    public static class OAuthTool
    {
        public static async  Task<OAuthClass> GetOAuth()
        {
            using FileStream openStream = File.OpenRead("Jsons/OAuth.json");
            OAuthClass? OAuth =
                await JsonSerializer.DeserializeAsync<OAuthClass>(openStream);
            return OAuth;
        }
        

}
}

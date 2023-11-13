using Secret_Santa_MVC.TelegramLog.Commands;
using Secret_Santa_MVC.TelegramLog.Data;
using Telegram.Bot;

namespace Secret_Santa_MVC.TelegramLog
{
    public class Bot
    {
        private static TelegramBotClient client { get; set; }
         private static List<Command> commandsList;

         public static IReadOnlyList<Command> Command => commandsList.AsReadOnly();

         public static async Task<TelegramBotClient> Get()
         {
             if(client != null) 
             { 
                 return client;
             }

             commandsList = new List<Command>();
             commandsList.Add(new HelloCommand());
             commandsList.Add(new StatusCommand());
            //TODO: Add more commands

            //Webhook 
            string webhook =
    $"https://api.telegram.org/bot{AppSettings.Key}/setWebhook?url={AppSettings.Url}/api/message/update";

            client = new TelegramBotClient(AppSettings.Key);

            await client.SetWebhookAsync(webhook);

             return client;
         }
     
    }
}

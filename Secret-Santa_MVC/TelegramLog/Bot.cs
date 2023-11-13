using Secret_Santa_MVC.TelegramLog.Commands;
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
             //TODO: Add more commands

             client = new TelegramBotClient(AppSettings.Key);

            var hook = string.Format(AppSettings.Url, "api/message/update");
            await client.SetWebhookAsync(hook);

             return client;
         }
     
    }
}

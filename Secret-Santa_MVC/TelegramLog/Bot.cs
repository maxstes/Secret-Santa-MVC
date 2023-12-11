using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Secret_Santa_MVC.TelegramLog.Commands;
using Secret_Santa_MVC.TelegramLog.Data;
using Telegram.Bot;

namespace Secret_Santa_MVC.TelegramLog
{
    public class Bot
    {
        //TODO: try create services with this class
        private TelegramBotClient client;
        public async Task<TelegramBotClient> Get()
         {
            
             if(client != null) 
             { 
                 return client;
             }

            //Webhook 
            string webhook =
    $"https://api.telegram.org/bot{AppSettings.Key}/setWebhook?url={AppSettings.Url}/api/message/update";

            client = new TelegramBotClient(AppSettings.Key);

            Thread.Sleep(3000);
            await client.SetWebhookAsync(webhook);

            return client;
         }
     
    }
}

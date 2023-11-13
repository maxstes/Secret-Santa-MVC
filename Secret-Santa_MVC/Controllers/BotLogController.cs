using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.TelegramLog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Secret_Santa_MVC.Controllers
{
    //TODO: move the  logbot to a separate application
     //webhook uri part
    public class BotLogController : ControllerBase
    {
    [Route(@"api/message/update")]
        public async Task<OkResult> Update([FromBody]Update update)
        {
            var commands = Bot.Command;
            var message = update.Message;
            var client = await Bot.Get();

            foreach(var command in commands)
            {
                if(command.Contains(message.Text))
                {
                    command.Execute(message,client);
                    break;
                }
            }
            return Ok(); 
        }
    }
}

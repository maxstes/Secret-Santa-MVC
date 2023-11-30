using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.TelegramLog;
using Secret_Santa_MVC.TelegramLog.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
            if (message == null)
            {
                return Ok();
            }
            var client = await Bot.Get();
            MessageType? messageType = update.Message.Type;

            if(messageType != MessageType.Text)
            {
                return Ok();
            }
            

             await SelectCommand(message,commands, client);
            return Ok(); 
        }
        public async Task SelectCommand(Message message,IReadOnlyList<BaseCommand> commands,TelegramBotClient client)
        {
            foreach (var command in commands)
            {

                if (command.Contains(message.Text))
                {
                    command.Execute(message, client);
                    break;
                }
            }
        }
    }
}

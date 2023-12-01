using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.TelegramLog;
using Secret_Santa_MVC.TelegramLog.Commands;
using Secret_Santa_MVC.TelegramLog.Commands.CommandExecutor;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Secret_Santa_MVC.Controllers
{
    //TODO: move the  logbot to a separate application
    //webhook uri part
    public class BotLogController : ControllerBase
    {
        private readonly ICommandExecutor _commandExecutor;
        public BotLogController(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }
        [Route(@"api/message/update")]
        public async Task<OkResult> Update([FromBody] Update update)
        {

            MessageType? messageType = update.Message.Type;

            if (messageType != MessageType.Text || update.Message == null)
            {
                return Ok();
            }
            await _commandExecutor.Execute(update);
            return Ok();

        }
    }
}
